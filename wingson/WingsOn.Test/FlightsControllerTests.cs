using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WingsOn.Api.Controllers;
using WingsOn.Api.Models;
using WingsOn.Bll.Services;
using WingsOn.Exceptions;
using WingsOn.Test.Fixtures;
using Xunit;

namespace WingsOn.Test
{
    public class FlightsControllerTests : WingsObTestsInitialize
    {
        private readonly FlightsController _flightsController;

        public FlightsControllerTests(DependenciesFixture dependenciesFixture)
            : base(dependenciesFixture)
        {
            var flightService = ServiceProvider.GetService<IFlightService>();
            _flightsController = new FlightsController(flightService);
        }

        [Fact]
        public void Get_ReturnsAllItems()
        {
            var result = (_flightsController.Get()?.Result as JsonResult)?.Value as List<FlightDto>;

            Assert.Equal(6, result?.Count);

            var ids = result?.Select(item => item.Id);

            Assert.Equal(new [] {1, 2, 3, 4, 5, 6}, ids);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(6)]
        public void Get_ExistingIdSpecified_ReturnsJsonResult(int id)
        {
            var result = _flightsController.Get(id)?.Result;

            Assert.IsType<JsonResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(3)]
        [InlineData(6)]
        public void Get_ExistingIdSpecified_ReturnsSpecificItem(int id)
        {
            var result = _flightsController.Get(id)?.Result as JsonResult;

            var value = result?.Value as FlightDto;

            Assert.Equal(id, value?.Id);
        }

        [Theory]
        [InlineData(10)]
        [InlineData(9999)]
        [InlineData(-1)]
        public void Get_NonexistingIdSpecified_ThrowsResourceNotFoundException(int id)
        {
            Assert.Throws<ResourceNotFoundException>(() => _flightsController.Get(id));
        }

        [Theory]
        [InlineData("A1")]
        [InlineData("B2")]
        [InlineData("C3")]
        public void Get_ExistingNumberSpecified_ReturnsJsonResult(string number)
        {
            var result = _flightsController.Get(number)?.Result;

            Assert.IsType<JsonResult>(result);
        }

        [Theory]
        [InlineData("A1")]
        [InlineData("B2")]
        [InlineData("C3")]
        public void Get_ExistingNumberSpecified_ReturnsSpecificItem(string number)
        {
            var result = _flightsController.Get(number)?.Result as JsonResult;

            var value = result?.Value as FlightDto;

            Assert.Equal(number, value?.Number);
        }

        [Theory]
        [InlineData("A2")]
        [InlineData("XX")]
        [InlineData("")]
        public void Get_NonexistingNumberSpecified_ThrowsResourceNotFoundException(string number)
        {
            Assert.Throws<ResourceNotFoundException>(() => _flightsController.Get(number));
        }

        [Theory]
        [InlineData("A1")]
        [InlineData("B2")]
        [InlineData("C3")]
        public void GetFlightPassengers_ExistingNumberSpecified_ReturnsJsonResult(string number)
        {
            var result = _flightsController.GetFlightPassengers(number)?.Result;

            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public void GetFlightPassengers_ExistingNumberSpecified_ReturnsAllPassengers()
        {
            var result = (_flightsController.GetFlightPassengers("C3")?.Result as JsonResult)?.Value as List<PersonDto>;

            var expectedResult = new[] {"Claire Stephens", "Kendall Velazquez", "Zenia Stout", "Bonnie Rice", "Louise Harper"};

            Assert.Equal(expectedResult, result?.Select(person => person.Name));
        }

        [Theory]
        [InlineData("A2")]
        [InlineData("XX")]
        [InlineData("")]
        public void GetFlightPassengers_NonexistingNumberSpecified_ThrowsResourceNotFoundException(string number)
        {
            Assert.Throws<ResourceNotFoundException>(() => _flightsController.GetFlightPassengers(number));
        }
    }
}
