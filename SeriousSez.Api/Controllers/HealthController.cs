using Microsoft.AspNetCore.Mvc;

namespace SeriousSez.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return new OkResult();
        }

        [HttpGet("ping2")]
        public IActionResult Ping2()
        {
            return new OkResult();
        }
    }
}
