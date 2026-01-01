using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeriousSez.ApplicationService.Services;
using SeriousSez.Domain.Models;
using SeriousSez.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriousSez.Api.Controllers
{
    [Route("[controller]")]
    public class IngredientController : Controller
    {
        private readonly ILogger<IngredientController> _logger;
        private readonly IIngredientService _ingredientService;
        private readonly IImageService _imageService;

        public IngredientController(ILogger<IngredientController> logger, IIngredientService ingredientService, IImageService imageService)
        {
            _logger = logger;
            _ingredientService = ingredientService;
            _imageService = imageService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] IngredientViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ingredient = await _ingredientService.Create(model);
            if (ingredient == null)
                return BadRequest("Failed to create Ingredient!");

            if (model.Image != null)
                await _imageService.Create(model.Image);

            return new OkResult();
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var ingredients = await _ingredientService.GetAll();
            if (ingredients == null)
            {
                _logger.LogError("Failed to fetch Ingredients!");
                return new NotFoundObjectResult("Failed to fetch Ingredients!");
            }

            _logger.LogTrace("Ingredients fetched!", ingredients);
            return new OkObjectResult(ingredients);
        }

        [HttpPost("update")]
        public async Task<IActionResult> Update([FromBody] IngredientResponse ingredient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _ingredientService.Update(ingredient);

            _logger.LogTrace("Ingredient has been updated!", result);

            return new OkObjectResult(result);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] List<IngredientResponse> ingredients)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var ingredient in ingredients)
            {
                await _ingredientService.Delete(ingredient);

                if (ingredient.Image != null)
                    await _imageService.Delete(ingredient.Image);
            }

            _logger.LogTrace("Ingredients have been deleted!", ingredients);

            var cleanedUsers = await _ingredientService.GetAll();

            return new OkObjectResult(cleanedUsers);
        }
    }
}
