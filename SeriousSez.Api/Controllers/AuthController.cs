using Microsoft.AspNetCore.Mvc;
using SeriousSez.ApplicationService.Services;
using SeriousSez.Domain.Models;
using System.Threading.Tasks;

namespace SeriousSez.Api.Controllers
{
    [Route("[controller]")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] CredentialsViewModel credentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _authService.Login(credentials);
            if(response == null)
                return BadRequest(("Login Failure", "Invalid username or password.", ModelState));

            //var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(response);
        }
    }
}
