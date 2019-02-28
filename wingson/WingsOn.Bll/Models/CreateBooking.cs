using System.Collections.Generic;
using WingsOn.Domain;

namespace WingsOn.Bll.Models
{
    public class CreateBooking
    {
        public int FlightId { get; set; }

        public IEnumerable<Person> Passengers { get; set; }
    }
}
