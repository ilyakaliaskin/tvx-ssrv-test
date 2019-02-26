using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.Models;
using WingsOn.Bll.Services;

namespace WingsOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;

        public FlightsController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var flights = Mapper.Map<List<FlightDto>>(_flightService.GetFlights());
            return new JsonResult(flights);
        }

        [HttpGet("{id:int}")]
        public ActionResult Get(int id)
        {
            var flight = Mapper.Map<FlightDto>(_flightService.GetFlight(id));
            return new JsonResult(flight);
        }

        [HttpGet("{number}")]
        public ActionResult Get(string number)
        {
            var flight = Mapper.Map<FlightDto>(_flightService.GetFlight(number));
            return new JsonResult(flight);
        }

        [HttpGet("{number}/passengers")]
        public ActionResult GetFlightPassengers(string number)
        {
            var flight = Mapper.Map<List<PersonDto>>(_flightService.GetFlightPassengers(number));
            return new JsonResult(flight);
        }
    }
}