using Microsoft.AspNetCore.Mvc;

namespace WingsOn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {

        [HttpGet]
        [Produces("text/plain")]
        public string Get()
        {
            return "Ok";
        }
    }
}