using System;
using System.Collections.Generic;

namespace WingsOn.Api.Models
{
    public class BookingDto
    {
        public int Id { get; set; }

        public FlightDto Flight { get; set; }

        public PersonDto Customer { get; set; }

        public IEnumerable<PersonDto> Passengers { get; set; }
        
        public DateTime DateBooking { get; set; }
    }
}
