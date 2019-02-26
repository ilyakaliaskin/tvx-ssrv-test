using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.Models;
using WingsOn.Api.Queries;
using WingsOn.Bll.SearchCriteria;
using WingsOn.Bll.Services;

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
        public ActionResult Get([FromQuery] PassengerQuery passengerQuery)
        {
            var passengerSearch = Mapper.Map<PassengerQuery, PersonSearchCriterion>(passengerQuery);
            var passengers = Mapper.Map<List<PersonDto>>(_passengerService.GetPassengers(passengerSearch));
            return new JsonResult(passengers);
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var passenger = Mapper.Map<PersonDto>(_passengerService.GetPassenger(id));
            return new JsonResult(passenger);
        }
    }
}