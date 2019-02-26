namespace WingsOn.Api.Models
{
    public class CreateBookingDto
    {
        public int FlightId { get; set; }

        public PersonDto Passenger { get; set; }
    }
}
