using System.Collections.Generic;

namespace WingsOn.Api.Models
{
    public class CreateBookingDto
    {
        public int FlightId { get; set; }

        public IEnumerable<PersonDto> Passengers { get; set; }
    }
}
