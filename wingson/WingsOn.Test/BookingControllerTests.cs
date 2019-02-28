using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WingsOn.Api.Controllers;
using WingsOn.Api.Models;
using WingsOn.Bll.Services;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.Test.Fixtures;
using Xunit;

namespace WingsOn.Test
{
    public class BookingControllerTests : WingsObTestsInitialize
    {
        private readonly IRepository<Booking> _bookingRepository;

        private readonly IRepository<Person> _personRepository;

        private readonly BookingsController _bookingsController;

        public BookingControllerTests(DependenciesFixture dependenciesFixture)
            : base(dependenciesFixture)
        {
            _bookingRepository = ServiceProvider.GetService<IRepository<Booking>>();

            _personRepository = ServiceProvider.GetService<IRepository<Person>>();

            var bookingService = ServiceProvider.GetService<IBookingService>();

            _bookingsController = new BookingsController(bookingService);
        }

        [Fact]
        public void Get_ReturnsAllItems()
        {
            var result = _bookingsController.Get()?.Result as JsonResult;

            var value = result?.Value as List<BookingDto>;

            Assert.Equal(4, value?.Count);

            var ids = value?.Select(item => item.Id);

            Assert.Equal(new [] {1, 2, 3, 4}, ids);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Get_ExistingIdSpecified_ReturnsJsonResult(int id)
        {
            var result = _bookingsController.Get(id)?.Result;

            Assert.IsType<JsonResult>(result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void Get_ExistingIdSpecified_ReturnsSpecificItem(int id)
        {
            var result = _bookingsController.Get(id)?.Result as JsonResult;

            var value = result?.Value as BookingDto;

            Assert.Equal(id, value?.Id);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(9999)]
        [InlineData(-1)]
        public void Get_NonexistingIdSpecified_ReturnsNotFound(int id)
        {
            var result = _bookingsController.Get(id)?.Result;

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Post_ValidBookingPassed_ReturnsJsonResult()
        {
            var createBookingDto = new CreateBookingDto
            {
                FlightId = 1,
                Passenger = new PersonDto()
            };

            var result = _bookingsController.Post(createBookingDto)?.Result;

            Assert.IsType<JsonResult>(result);
        }

        [Fact]
        public void Post_ValidBookingPassed_CreatesNewBooking()
        {
            // Get the list of bookings before creation
            var bookingsBeforeCreation = _bookingRepository.GetAll().ToList();

            var passengerName = "New Passenger Test";
            var flightId = 1;

            var createBookingDto = new CreateBookingDto
            {
                FlightId = flightId,
                Passenger = new PersonDto
                {
                    Name = passengerName
                }
            };

            // Create new booking
            _bookingsController.Post(createBookingDto);

            // Get the list of bookings after creation
            var bookingsAfterCreation = _bookingRepository.GetAll().ToList();

            // Verify that the difference between old and new lists of bookings is the newly created booking.
            var difference = bookingsAfterCreation.Except(bookingsBeforeCreation).ToList();
            
            Assert.Single(difference);

            var newBooking = difference.FirstOrDefault();

            Assert.Equal(flightId, newBooking?.Flight?.Id);

            Assert.Equal(passengerName, newBooking?.Passengers?.FirstOrDefault()?.Name);
        }

        [Fact]
        public void Post_ValidBookingPassed_CreatesNewPerson()
        {
            // Get the people before creation
            var peopleBeforeCreation = _personRepository.GetAll().ToList();

            var passengerName = "New Passenger Test";
            var flightId = 1;

            var createBookingDto = new CreateBookingDto
            {
                FlightId = flightId,
                Passenger = new PersonDto
                {
                    Name = passengerName
                }
            };

            // Create new booking
            _bookingsController.Post(createBookingDto);

            // Get the people after creation
            var peopleAfterCreation = _personRepository.GetAll().ToList();

            // Verify that the difference between old and updated lists of persons.
            var difference = peopleAfterCreation.Except(peopleBeforeCreation).ToList();

            Assert.Single(difference);

            var newPassenger = difference.FirstOrDefault();

            Assert.Equal(passengerName, newPassenger?.Name);
        }

        [Fact]
        public void Post_ValidBookingPassed_ReturnsCreatedBooking()
        {
            // Get the list of bookings before creation
            var bookingsBeforeCreation = _bookingRepository.GetAll().ToList();

            var flightId = 1;

            var createBookingDto = new CreateBookingDto
            {
                FlightId = flightId,
                Passenger = new PersonDto()
            };

            // Create new booking
            var createdBooking = (_bookingsController.Post(createBookingDto)?.Result as JsonResult)?.Value as BookingDto;

            Assert.False(createdBooking == null);

            // Get the list of bookings after creation
            var bookingsAfterCreation = _bookingRepository.GetAll().ToList();

            // Verify that the difference between old and new list of bookings is the newly created booking.
            var difference = bookingsAfterCreation.Except(bookingsBeforeCreation).FirstOrDefault();

            Assert.False(difference == null);

            Assert.Equal(createdBooking.Id, difference.Id);
        }

        [Fact]
        public void Post_NonExistentFlightSpecified_ReturnsBadRequest()
        {
            var invalidBookingId = -1;

            var createBookingDto = new CreateBookingDto
            {
                FlightId = invalidBookingId,
                Passenger = new PersonDto()
            };

            var result = _bookingsController.Post(createBookingDto)?.Result;

            Assert.IsType<BadRequestResult>(result);
        }
    }
}
