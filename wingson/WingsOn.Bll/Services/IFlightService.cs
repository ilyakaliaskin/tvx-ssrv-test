using System.Collections.Generic;
using WingsOn.Domain;

namespace WingsOn.Bll.Services
{
    public interface IFlightService
    {
        IEnumerable<Flight> GetFlights();

        Flight GetFlight(int id);

        Flight GetFlight(string number);

        IEnumerable<Person> GetFlightPassengers(string number);
    }
}