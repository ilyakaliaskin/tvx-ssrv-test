using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using WingsOn.Api.Controllers;
using WingsOn.Api.Models;
using WingsOn.Bll.Services;
using WingsOn.Dal;
using WingsOn.Domain;
using WingsOn.Test.Fixtures;
using WingsOn.Test.Stubs;
using Xunit;

namespace WingsOn.Test
{
    [Collection("DependenciesCollection")]
    public class WingsObTestsInitialize
    {
        protected ServiceProvider ServiceProvider;

        public WingsObTestsInitialize(DependenciesFixture dependenciesFixture)
        {
            var services = dependenciesFixture.Services;

            InitializeRepositories(services);

            ServiceProvider = services.BuildServiceProvider();
        }

        private void InitializeRepositories(ServiceCollection services)
        {
            services.AddSingleton<IRepository<Person>, PersonRepositoryStub>();
            services.AddSingleton<IRepository<Flight>, FlightRepositoryStub>();
            services.AddSingleton<IRepository<Booking>, BookingRepositoryStub>();
            services.AddSingleton<IRepository<Airport>, AirportRepositoryStub>();
            services.AddSingleton<IRepository<Airline>, AirlineRepositoryStub>();
        }
    }
}
