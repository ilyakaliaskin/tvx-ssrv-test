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
        public ActionResult<List<FlightDto>> Get()
        {
            var flights = Mapper.Map<List<FlightDto>>(_flightService.GetFlights());
            return new JsonResult(flights);
        }

        [HttpGet("{id:int}")]
        public ActionResult<FlightDto> Get(int id)
        {
            var flight = Mapper.Map<FlightDto>(_flightService.GetFlight(id));

            if (flight == null)
            {
                return NotFound();
            }

            return new JsonResult(flight);
        }

        [HttpGet("{number}")]
        public ActionResult<FlightDto> Get(string number)
        {
            var flight = Mapper.Map<FlightDto>(_flightService.GetFlight(number));

            if (flight == null)
            {
                return NotFound();
            }

            return new JsonResult(flight);
        }

        [HttpGet("{number}/passengers")]
        public ActionResult<List<PersonDto>> GetFlightPassengers(string number)
        {
            var passengers = Mapper.Map<List<PersonDto>>(_flightService.GetFlightPassengers(number));
            
            if (passengers == null)
            {
                return NotFound();
            }

            return new JsonResult(passengers);
        }
    }
}