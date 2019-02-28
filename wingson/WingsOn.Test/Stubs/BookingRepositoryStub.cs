using System;
using System.Globalization;
using System.Linq;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Test.Stubs
{
    public class BookingRepositoryStub : RepositoryBase<Booking>
    {
        public BookingRepositoryStub() 
        {
            PersonRepositoryStub persons = new PersonRepositoryStub();
            FlightRepositoryStub flights = new FlightRepositoryStub();
            CultureInfo cultureInfo = new CultureInfo("nl-NL");

            Repository.AddRange(new []
            {
                new Booking
                {
                    Id = 1,
                    Number = "WO-1",
                    Customer = persons.GetAll().Single(p => p.Name == "Branden Johnston"),
                    DateBooking = DateTime.Parse("03/03/2006 14:30", cultureInfo),
                    Flight = flights.GetAll().Single(f => f.Id == 2),
                    Passengers = new []
                    {
                        persons.GetAll().Single(p => p.Name == "Branden Johnston")
                    }
                },
                new Booking
                {
                    Id = 2,
                    Number = "WO-2",
                    Customer = persons.GetAll().Single(p => p.Name == "Debra Lang"),
                    DateBooking = DateTime.Parse("12/02/2000 12:55", cultureInfo),
                    Flight = flights.GetAll().Single(f => f.Id == 3),
                    Passengers = new []
                    {
                        persons.GetAll().Single(p => p.Name == "Claire Stephens"),
                        persons.GetAll().Single(p => p.Name == "Kendall Velazquez"),
                        persons.GetAll().Single(p => p.Name == "Zenia Stout")
                    }
                },
                new Booking
                {
                    Id = 3,
                    Number = "WO-3",
                    Customer = persons.GetAll().Single(p => p.Name == "Kathy Morgan"),
                    DateBooking = DateTime.Parse("13/02/2000 16:37", cultureInfo),
                    Flight = flights.GetAll().Single(f => f.Id == 6),
                    Passengers = new []
                    {
                        persons.GetAll().Single(p => p.Name == "Kathy Morgan"),
                        persons.GetAll().Single(p => p.Name == "Melissa Long")
                    }
                },
                new Booking
                {
                    Id = 4,
                    Number = "WO-4",
                    Customer = persons.GetAll().Single(p => p.Name == "Bonnie Rice"),
                    DateBooking = DateTime.Parse("03/12/2011 16:50", cultureInfo),
                    Flight = flights.GetAll().Single(f => f.Id == 3),
                    Passengers = new []
                    {
                        persons.GetAll().Single(p => p.Name == "Bonnie Rice"),
                        persons.GetAll().Single(p => p.Name == "Louise Harper")
                    }
                }
            });
        }
    }
}
