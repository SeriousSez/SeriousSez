using Microsoft.AspNetCore.Mvc;

namespace SeriousSez.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HealthController : ControllerBase
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return new OkResult();
        }

        [HttpGet("ping3")]
        public IActionResult Ping2()
        {
            return new OkResult();
        }
    }
}
