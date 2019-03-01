using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.Models;
using WingsOn.Bll.Models;
using WingsOn.Bll.Services;

namespace WingsOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet]
        public ActionResult<List<BookingDto>> Get()
        {
            var bookings = Mapper.Map<List<BookingDto>>(_bookingService.GetBookings());

            return new JsonResult(bookings);
        }

        [HttpGet("{id}")]
        public ActionResult<BookingDto> Get(int id)
        {
            var booking = Mapper.Map<BookingDto>(_bookingService.GetBooking(id));

            return new JsonResult(booking);
        }

        [HttpPost]
        public ActionResult<BookingDto> Post([FromBody] CreateBookingDto createBookingDto)
        {
            var createBooking = Mapper.Map<CreateBooking>(createBookingDto);

            var booking = Mapper.Map<BookingDto>(_bookingService.CreateBooking(createBooking));

            return new JsonResult(booking);
        }
    }
}
