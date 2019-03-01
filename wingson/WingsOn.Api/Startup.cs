using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WingsOn.Api.Middleware;
using WingsOn.Api.Models;
using WingsOn.Api.Queries;
using WingsOn.Bll.Models;
using WingsOn.Bll.SearchCriteria;
using WingsOn.Bll.Services;
using WingsOn.Dal;
using WingsOn.Domain;

namespace WingsOn.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            ConfigureMapper();

            ConfigureRepositories(services);

            ConfigureBllServices(services);
        }

        private void ConfigureBllServices(IServiceCollection services)
        {
            services.AddSingleton<IPassengerService, PassengerService>();
            services.AddSingleton<IFlightService, FlightService>();
            services.AddSingleton<IBookingService, BookingService>();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddSingleton<IRepository<Person>, PersonRepository>();
            services.AddSingleton<IRepository<Flight>, FlightRepository>();
            services.AddSingleton<IRepository<Booking>, BookingRepository>();
            services.AddSingleton<IRepository<Airport>, AirportRepository>();
            services.AddSingleton<IRepository<Airline>, AirlineRepository>();
        }

        private void ConfigureMapper()
        {
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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseMvc();
        }
    }
}
