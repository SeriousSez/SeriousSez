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
    }
}
