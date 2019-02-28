using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using WingsOn.Api.Models;
using WingsOn.Api.Queries;
using WingsOn.Bll.Models;
using WingsOn.Bll.SearchCriteria;
using WingsOn.Bll.Services;
using WingsOn.Domain;
using Xunit;

namespace WingsOn.Test.Fixtures
{
    public class DependenciesFixture
    {
        public readonly ServiceCollection Services;

        public DependenciesFixture()
        {
            Services = new ServiceCollection();

            Services.AddSingleton<IPassengerService, PassengerService>();
            Services.AddSingleton<IFlightService, FlightService>();
            Services.AddSingleton<IBookingService, BookingService>();

            Mapper.Initialize(cfg => {
                cfg.CreateMap<Person, PersonDto>();
                cfg.CreateMap<PersonDto, Person>();

                cfg.CreateMap<Flight, FlightDto>()
                    .ForMember(dest => dest.AirlineCode, opts => opts.MapFrom(src => src.Carrier.Code))
                    .ForMember(dest => dest.DepartureAirportCode, opts => opts.MapFrom(src => src.DepartureAirport.Code))
                    .ForMember(dest => dest.ArrivalAirportCode, opts => opts.MapFrom(src => src.ArrivalAirport.Code));

                cfg.CreateMap<PassengerQuery, PersonSearchCriterion>();

                cfg.CreateMap<Booking, BookingDto>();
                cfg.CreateMap<CreateBookingDto, CreateBooking>();
            });
        }
    }

    [CollectionDefinition("DependenciesCollection")]
    public class DependenciesCollection : ICollectionFixture<DependenciesFixture>
    {
    }
}
