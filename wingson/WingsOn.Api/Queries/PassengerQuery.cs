using Microsoft.AspNetCore.Mvc;
using WingsOn.Api.Models;

namespace WingsOn.Api.Queries
{
    public class PassengerQuery
    {
        [FromQuery(Name = "gender")]
        public GenderType? Gender { get; set; }
    }
}
