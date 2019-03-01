using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WingsOn.Api.Controllers;
using WingsOn.Api.Models;
using WingsOn.Api.Queries;
using WingsOn.Bll.Services;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.Test.Fixtures;
using Xunit;
using GenderType = WingsOn.Api.Models.GenderType;

namespace WingsOn.Test
{
    public class PassengersControllerTests : WingsObTestsInitialize
    {
        private readonly PassengersController _passengersController;

        private readonly IRepository<Person> _personRepository;

        public PassengersControllerTests(DependenciesFixture dependenciesFixture)
            : base(dependenciesFixture)
        {
            var passengerService = ServiceProvider.GetService<IPassengerService>();
            _passengersController = new PassengersController(passengerService);

            _personRepository = ServiceProvider.GetService<IRepository<Person>>();
        }

        [Fact]
        public void Get_NoQuery_ReturnsAllItems()
        {
            var query = new PassengerQuery { Gender = null };

            var result = (_passengersController.Get(query)?.Result as JsonResult)?.Value as List<PersonDto>;

            Assert.Equal(11, result?.Count);

            var ids = result?.Select(item => item.Id);

            Assert.Equal(new [] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11}, ids);
        }

        [Theory]
        [InlineData(GenderType.Male, 4)]
        [InlineData(GenderType.Female, 7)]
        public void Get_QueryByGender_ReturnsMatchingItems(GenderType gender, int expectedNumber)
        {
            var query = new PassengerQuery { Gender = gender };

            var result = (_passengersController.Get(query)?.Result as JsonResult)?.Value as List<PersonDto>;

            Assert.Equal(expectedNumber, result?.Count);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(6)]
        public void Get_ExistingIdSpecified_ReturnsJsonResult(int id)
        {
            var result = _passengersController.Get(id)?.Result;

            Assert.IsType<JsonResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(6)]
        public void Get_ExistingIdSpecified_ReturnsSpecificItem(int id)
        {
            var result = _passengersController.Get(id)?.Result as JsonResult;

            var value = result?.Value as PersonDto;

            Assert.Equal(id, value?.Id);
        }

        [Theory]
        [InlineData(20)]
        [InlineData(9999)]
        [InlineData(-1)]
        public void Get_NonexistingIdSpecified_ReturnsNotFound(int id)
        {
            var result = _passengersController.Get(id)?.Result;

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Put_ExistingIdSpecified_UpdatesPerson()
        {
            var personId = 1;

            var personNameBeforeUpdate = _personRepository.Get(personId).Name;

            Assert.Equal("Kendall Velazquez", personNameBeforeUpdate);

            var newPassengerName = "Updated Passenger Test";

            var person = new PersonDto
            {
                Name = newPassengerName
            };

            _passengersController.Put(personId, person);

            var personNameAfterUpdate = _personRepository.Get(personId).Name;

            Assert.Equal(newPassengerName, personNameAfterUpdate);
        }

        [Fact]
        public void Put_NotExistingIdSpecified_CreatesPerson()
        {
            var personId = 12;

            var nonExistentPerson = _personRepository.Get(personId);

            Assert.Null(nonExistentPerson);

            var newPassengerName = "New Passenger Test";

            var person = new PersonDto
            {
                Name = newPassengerName
            };

            _passengersController.Put(personId, person);

            var personAfterUpdate = _personRepository.Get(personId);

            Assert.NotNull(personAfterUpdate);

            Assert.Equal(personId, personAfterUpdate.Id);

            Assert.Equal(newPassengerName, personAfterUpdate.Name);
        }

        [Fact]
        public void Patch_ExistingIdSpecified_UpdatesPerson()
        {
            var personId = 1;

            var personAddressBeforeUpdate = _personRepository.Get(personId).Address;

            Assert.Equal("805-1408 Mi Rd.", personAddressBeforeUpdate);

            var newPassengerAddress = "Updated Address Test";

            var patch = new JsonPatchDocument<PersonDto>();
            patch.Replace(personDto => personDto.Address, newPassengerAddress);

            _passengersController.Patch(personId, patch);

            var personAddressAfterUpdate = _personRepository.Get(personId).Address;

            Assert.Equal(newPassengerAddress, personAddressAfterUpdate);
        }

        [Fact]
        public void Patch_NotExistingIdSpecified_ReturnsNotFound()
        {
            var personId = 12;

            var patch = new JsonPatchDocument<PersonDto>();

            var result = _passengersController.Patch(personId, patch);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
