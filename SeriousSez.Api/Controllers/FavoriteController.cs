using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeriousSez.ApplicationService.Services;
using SeriousSez.Domain.Models;
using System.Threading.Tasks;

namespace SeriousSez.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class FavoriteController : Controller
    {
        private readonly ILogger<FavoriteController> _logger;
        private readonly IFavoriteService _favoriteService;

        public FavoriteController(ILogger<FavoriteController> logger, IFavoriteService favoriteService)
        {
            _logger = logger;
            _favoriteService = favoriteService;
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(string username)
        {
            var favorites = await _favoriteService.Get(username);

            _logger.LogTrace("Favorites fetched! Favorites: {@Favorites}", favorites);
            return new OkObjectResult(favorites);
        }

        [HttpGet("isfavored")]
        public async Task<IActionResult> IsFavored(string username, string title, string creator)
        {
            var favorites = await _favoriteService.IsFavored(username, title, creator);

            _logger.LogTrace("Favorites fetched! Favorites: {@Favorites}", favorites);
            return new OkObjectResult(favorites);
        }

        [HttpPost("recipe")]
        public async Task<IActionResult> Recipe([FromBody] FavoriteRecipeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ingredient = await _favoriteService.Recipe(model);
            if (ingredient == null)
                return BadRequest("Failed to add Recipe to Favorites!");

            return new OkResult();
        }

        [HttpPost("ingredient")]
        public async Task<IActionResult> Ingredient([FromBody] FavoriteIngredientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ingredient = await _favoriteService.Ingredient(model);
            if (ingredient == null)
                return BadRequest("Failed to add Ingredient to Favorites!");

            return new OkResult();
        }
    }
}
