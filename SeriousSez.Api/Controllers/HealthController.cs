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
    }
}
