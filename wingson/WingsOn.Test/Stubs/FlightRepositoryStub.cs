using System;
using System.Globalization;
using System.Linq;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Test.Stubs
{
    public class FlightRepositoryStub : RepositoryBase<Flight>
    {
        public FlightRepositoryStub() 
        {
            AirportRepositoryStub airports = new AirportRepositoryStub();
            AirlineRepositoryStub airlines = new AirlineRepositoryStub();
            CultureInfo cultureInfo = new CultureInfo("nl-NL");

            Repository.AddRange(new []
            {
                new Flight
                {
                    Id = 1,
                    Number = "A1",
                    DepartureAirport = airports.GetAll().Single(a => a.Id == 1),
                    DepartureDate = DateTime.Parse("12/02/2012 16:50", cultureInfo),
                    ArrivalAirport =  airports.GetAll().Single(a => a.Id == 3),
                    ArrivalDate = DateTime.Parse("13/02/2012 00:00", cultureInfo),
                    Carrier = airlines.GetAll().Single(a => a.Id == 1),
                    Price = 196.1m
                },
                new Flight
                {
                    Id = 2,
                    Number = "B2",
                    DepartureAirport = airports.GetAll().Single(a => a.Id == 4),
                    DepartureDate = DateTime.Parse("20/02/2000 17:50", cultureInfo),
                    ArrivalAirport =  airports.GetAll().Single(a => a.Id == 2),
                    ArrivalDate = DateTime.Parse("20/02/2000 19:00", cultureInfo),
                    Carrier = airlines.GetAll().Single(a => a.Id == 2),
                    Price = 95.2m
                },
                new Flight
                {
                    Id = 3,
                    Number = "C3",
                    ArrivalAirport = airports.GetAll().Single(a => a.Id == 3),
                    ArrivalDate = DateTime.Parse("20/02/2000 17:50", cultureInfo),
                    DepartureAirport =  airports.GetAll().Single(a => a.Id == 1),
                    DepartureDate = DateTime.Parse("20/02/2000 19:00", cultureInfo),
                    Carrier = airlines.GetAll().Single(a => a.Id == 1),
                    Price = 57.6m
                },
                new Flight
                {
                    Id = 4,
                    Number = "D4",
                    ArrivalAirport = airports.GetAll().Single(a => a.Id == 2),
                    ArrivalDate = DateTime.Parse("17/07/2009 11:10", cultureInfo),
                    DepartureAirport =  airports.GetAll().Single(a => a.Id == 4),
                    DepartureDate = DateTime.Parse("17/07/2009 13:45", cultureInfo),
                    Carrier = airlines.GetAll().Single(a => a.Id == 2),
                    Price = 185m
                },
                new Flight
                {
                    Id = 5,
                    Number = "E5",
                    ArrivalAirport = airports.GetAll().Single(a => a.Id == 3),
                    ArrivalDate = DateTime.Parse("28/05/2008 20:10", cultureInfo),
                    DepartureAirport =  airports.GetAll().Single(a => a.Id == 4),
                    DepartureDate = DateTime.Parse("29/05/2008 13:30", cultureInfo),
                    Carrier = airlines.GetAll().Single(a => a.Id == 1),
                    Price = 1140.5m
                },
                new Flight
                {
                    Id = 6,
                    Number = "F6",
                    ArrivalAirport = airports.GetAll().Single(a => a.Id == 4),
                    ArrivalDate = DateTime.Parse("14/11/2006 21:00", cultureInfo),
                    DepartureAirport =  airports.GetAll().Single(a => a.Id == 1),
                    DepartureDate = DateTime.Parse("15/11/2006 01:30", cultureInfo),
                    Carrier = airlines.GetAll().Single(a => a.Id == 2),
                    Price = 416.17m
                }
            });
        }
    }
}
