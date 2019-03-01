using System.Collections.Generic;
using System.Linq;
using WingsOn.Bll.Models;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.Exceptions;

namespace WingsOn.Bll.Services
{
    public class BookingService : IBookingService
    {
        private readonly IRepository<Booking> _bookingRepository;
        private readonly IRepository<Flight> _flightRepository;
        private readonly IPassengerService _passengerService;

        public BookingService(
            IRepository<Booking> bookingRepository,
            IRepository<Flight> flightRepository,
            IPassengerService passengerService
        )
        {
            _bookingRepository = bookingRepository;
            _flightRepository = flightRepository;
            _passengerService = passengerService;
        }

        public IEnumerable<Booking> GetBookings()
        {
            return _bookingRepository.GetAll();
        }

        public Booking GetBooking(int id)
        {
            return _bookingRepository.Get(id)

                ?? throw new ResourceNotFoundException($"Booking with the Id specified does not exist: {id}.");
        }

        public Booking CreateBooking(CreateBooking createBooking)
        {
            var bookingId = GetNewBookingId();

            var flight = _flightRepository.Get(createBooking.FlightId)

                ?? throw new InvalidRequestDataException($"Failed to create new booking, flight with the Id specified does not exist: {createBooking.FlightId}");

            var createdPassengers = new List<Person>();

            foreach (var passenger in createBooking.Passengers)
            {
                createdPassengers.Add(_passengerService.CreatePassenger(passenger));
            }

            var booking = new Booking
            {
                Id = bookingId,
                Flight = flight,
                Passengers = createdPassengers
            };
            
            _bookingRepository.Save(booking);

            return GetBooking(bookingId);
        }

        private int GetNewBookingId()
        {
            return _bookingRepository.GetAll().Max(booking => booking.Id) + 1;
        }
    }
}
