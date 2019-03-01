using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.Models;
using WingsOn.Api.Queries;
using WingsOn.Bll.SearchCriteria;
using WingsOn.Bll.Services;
using WingsOn.Domain;

namespace WingsOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengersController : ControllerBase
    {
        private readonly IPassengerService _passengerService;

        public PassengersController(IPassengerService passengerService)
        {
            _passengerService = passengerService;
        }

        [HttpGet]
        public ActionResult<List<PersonDto>> Get([FromQuery] PassengerQuery passengerQuery)
        {
            var passengerSearch = Mapper.Map<PassengerQuery, PersonSearchCriterion>(passengerQuery);

            var passengers = Mapper.Map<List<PersonDto>>(_passengerService.GetPassengers(passengerSearch));

            return new JsonResult(passengers);
        }

        [HttpGet("{id}")]
        public ActionResult<PersonDto> Get(int id)
        {
            var passenger = Mapper.Map<PersonDto>(_passengerService.GetPassenger(id));

            return new JsonResult(passenger);
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] PersonDto personDto)
        {
            var passenger = Mapper.Map<Person>(personDto);

            _passengerService.UpdatePassenger(id, passenger);

            return Ok();
        }

        [HttpPatch("{id}")]
        public ActionResult Patch(int id, [FromBody] JsonPatchDocument<PersonDto> personPatch)
        {
            var personDto = Mapper.Map<PersonDto>(_passengerService.GetPassenger(id));

            personPatch.ApplyTo(personDto);

            var person = Mapper.Map<Person>(personDto);

            _passengerService.UpdatePassenger(id, person);

            return Ok();
        }
    }
}