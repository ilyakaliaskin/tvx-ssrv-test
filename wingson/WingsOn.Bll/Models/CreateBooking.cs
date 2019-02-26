using WingsOn.Domain;

namespace WingsOn.Bll.Models
{
    public class CreateBooking
    {
        public int FlightId { get; set; }

        public Person Passenger { get; set; }
    }
}
