using System;

namespace WingsOn.Api.Models
{
    public class FlightDto
    {
        public int Id { get; set; }

        public string Number { get; set; }

        public string AirlineCode { get; set; }

        public string DepartureAirportCode { get; set; }

        public DateTime DepartureDate { get; set; }

        public string ArrivalAirportCode { get; set; }

        public DateTime ArrivalDate { get; set; }

        public decimal Price { get; set; }
    }
}
