using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeriousSez.ApplicationService.Services;
using SeriousSez.Domain.Models;
using SeriousSez.Domain.Responses;
using System;
using System.Threading.Tasks;

namespace SeriousSez.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUserService _userService;

        public AccountController(ILogger<AccountController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(string userName)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.GetByUserName(userName);
            if (user == null)
                return new NotFoundResult();

            return new OkObjectResult(user);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if(string.IsNullOrWhiteSpace(model.Role))
                model.Role = "User";

            var identityResult = await _userService.Create(model);
            if (identityResult.Succeeded == false)
                return new BadRequestObjectResult((identityResult, ModelState));

            return new OkResult();
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] UserUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userService.Update(model);
            if (user == null)
                return BadRequest("Failed to upgrade user!");

            _logger.LogTrace("User has been updated!", user);

            return new OkResult();
        }

        [HttpGet("getsettings")]
        public async Task<IActionResult> GetSettings(Guid userId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var settings = await _userService.GetSettings(userId);
            if (settings == null)
                return new NotFoundResult();

            return new OkObjectResult(settings);
        }

        [HttpPost("updatesettings")]
        public async Task<IActionResult> UpdateSettings([FromBody] UserSettingsUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var settings = await _userService.UpdateSettings(model);
            if (settings == null)
                return BadRequest("Failed to upgrade user settings!");

            _logger.LogTrace("User settings have been updated!", settings);

            return new OkResult();
        }
    }
}
