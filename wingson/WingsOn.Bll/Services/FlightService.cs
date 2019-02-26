using System.Collections.Generic;
using System.Linq;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Bll.Services
{
    public class FlightService : IFlightService
    {
        private readonly IRepository<Flight> _flightRepository;
        private readonly IRepository<Booking> _bookingRepository;

        public FlightService(
            IRepository<Flight> flightRepository,
            IRepository<Booking> bookingRepository
        )
        {
            _flightRepository = flightRepository;
            _bookingRepository = bookingRepository;
        }

        public IEnumerable<Flight> GetFlights()
        {
            return _flightRepository.GetAll();
        }

        public Flight GetFlight(int id)
        {
            return _flightRepository.Get(id);
        }

        public Flight GetFlight(string number)
        {
            return _flightRepository.GetAll().FirstOrDefault(flight => flight.Number == number);
        }

        public IEnumerable<Person> GetFlightPassengers(string number)
        {
            return _bookingRepository.GetAll().Where(booking => booking.Flight.Number == number).SelectMany(flight => flight.Passengers);
        }
    }
}
