using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SeriousSez.ApplicationService.Services;
using SeriousSez.Domain.Models;
using SeriousSez.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriousSez.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : Controller
    {
        private readonly ILogger<RecipeController> _logger;
        private readonly IRecipeService _recipeService;

        public RecipeController(ILogger<RecipeController> logger, IRecipeService recipeService)
        {
            _logger = logger;
            _recipeService = recipeService;
        }

        [HttpPost("create")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Create([FromBody] RecipeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var recipe = await _recipeService.Create(model);
            if (recipe == null) 
                return BadRequest("Failed to create recipe!");

            return new OkObjectResult(recipe);
        }

        [HttpPost("update")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> Update([FromBody] RecipeUpdateViewModel recipe)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var result = await _recipeService.Update(recipe);
             
            _logger.LogTrace("Recipe has been updated!", result);

            return new OkObjectResult(result);
        }

        [HttpPost("deleterecipeingredient")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> DeleteRecipeIngredient([FromBody] List<IngredientResponse> ingredients)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var ingredient in ingredients)
            {
                var result = await _recipeService.DeleteRecipeIngredient(ingredient);

                if (result == false)
                    return NotFound("Ingredient could not be found!");
            }

            _logger.LogTrace("Ingredients have been deleted!", ingredients);

            return new OkResult();
        }

        [HttpPost("addingredients")]
        [Authorize(AuthenticationSchemes = "Bearer")]
        public async Task<IActionResult> AddIngredients([FromBody] List<IngredientResponse> ingredients, string title, string creator)
        {
            var recipe = await _recipeService.AddIngredients(ingredients, title, creator);
            if (recipe == null)
                return BadRequest("Failed to add new ingredients to recipe!");

            return new OkObjectResult(recipe);
        }

        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] List<Guid> recipeIds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            foreach (var id in recipeIds)
            {
                var result = await _recipeService.Delete(id);
            }

            _logger.LogTrace("Recipes have been deleted!", recipeIds);

            var cleanedRecipes = await _recipeService.GetAll();

            return new OkObjectResult(cleanedRecipes);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get(string title, string creator)
        {
            var recipe = await _recipeService.Get(title, creator);
            if (recipe == null)
            {
                _logger.LogError("Failed to fetch recipe!");
                return new NotFoundObjectResult("Failed to fetch recipe!");
            }

            _logger.LogTrace("Recipe fetched!", recipe);
            return new OkObjectResult(recipe);
        }

        [HttpGet("getallbycreator")]
        public async Task<IActionResult> GetAllByCreator(string creator)
        {
            var recipes = await _recipeService.GetAll(creator);
            if (recipes == null)
            {
                _logger.LogError("Failed to fetch recipes!");
                return new NotFoundObjectResult("Failed to fetch recipes!");
            }

            _logger.LogTrace("Recipes fetched!", recipes);
            return new OkObjectResult(recipes);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var recipes = await _recipeService.GetAll();
            if (recipes == null)
            {
                _logger.LogError("Failed to fetch recipes!");
                return new NotFoundObjectResult("Failed to fetch recipes!");
            }

            _logger.LogTrace("Recipes fetched!", recipes);
            return new OkObjectResult(recipes);
        }

        [HttpGet("getallbyingredient")]
        public async Task<IActionResult> GetAllByIngredient(IngredientResponse ingredient)
        {
            var users = await _recipeService.GetAllByIngredient(ingredient);
            if (users == null)
            {
                _logger.LogError("Failed to fetch recipes by ingredient!");
                return new NotFoundObjectResult("Failed to fetch recipes by ingredient!");
            }

            _logger.LogTrace("Recipes fetched!", users);
            return new OkObjectResult(users);
        }
    }
}
