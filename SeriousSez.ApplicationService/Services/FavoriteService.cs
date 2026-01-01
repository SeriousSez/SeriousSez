using AutoMapper;
using Microsoft.Extensions.Logging;
using SeriousSez.Domain.Entities;
using SeriousSez.Domain.Entities.Recipe;
using SeriousSez.Domain.Models;
using SeriousSez.Domain.Responses;
using SeriousSez.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriousSez.ApplicationService.Services
{
    public class FavoriteService : IFavoriteService
    {
        private readonly ILogger<FavoriteService> _logger;
        private readonly IFavoriteRepository _favoriteRepository;
        private readonly IUserRepository _userRepository;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IRecipeRepository _recipeRepository;
        private readonly IMapper _mapper;

        public FavoriteService(
            ILogger<FavoriteService> logger, 
            IFavoriteRepository favoriteRepository, 
            IUserRepository userRepository, 
            IIngredientRepository ingredientRepository, 
            IRecipeRepository recipeRepository, 
            IMapper mapper)
        {
            _logger = logger;
            _favoriteRepository = favoriteRepository;
            _userRepository = userRepository;
            _ingredientRepository = ingredientRepository;
            _recipeRepository = recipeRepository;
            _mapper = mapper;
        }

        public async Task<FavoritesResponse> Get(string username)
        {
            var user = await _userRepository.GetByUserName(username);
            var favorites = await _favoriteRepository.GetByUserFull(user);

            return _mapper.Map<FavoritesResponse>(favorites);
        }

        public async Task<bool> IsFavored(string username, string title, string creator)
        {
            var user = await _userRepository.GetByUserName(username);

            var recipe = await _recipeRepository.GetByTitleAndCreator(title, creator);
            if (recipe == null)
                return false;

            var isFavored = await _favoriteRepository.RecipeFavoriteExists(user, recipe);
            return isFavored;
        }

        public async Task<Favorites> Create(User user)
        {
            return await _favoriteRepository.Create(new Favorites { User = user, Recipes = new List<Recipe>(), Ingredients = new List<Ingredient>() });
        }

        public async Task<FavoritesResponse> Recipe(FavoriteRecipeViewModel model)
        {
            var user = await _userRepository.GetByUserName(model.UserName);
            if (user == null)
                return null;

            var recipe = await _recipeRepository.GetByTitleAndCreator(model.Recipe.Title, model.Recipe.Creator);
            if (recipe == null)
                return null;

            var favorites = await _favoriteRepository.GetByUserFull(user);
            if (favorites == null)
                favorites = await Create(user);

            var exists = await _favoriteRepository.RecipeFavoriteExists(user, recipe);
            if (exists)
            {
                favorites.Recipes.Remove(recipe);
                _logger.LogTrace("Removed Recipe to Favorites!", favorites);
            }
            else
            {
                if (favorites.Recipes == null)
                    favorites.Recipes = new List<Recipe>();

                favorites.Recipes.Add(recipe);
                _logger.LogTrace("Added Recipe from Favorites!", favorites);
            }

            await _favoriteRepository.Update(favorites);

            return _mapper.Map<FavoritesResponse>(favorites);
        }

        public async Task<FavoritesResponse> Ingredient(FavoriteIngredientViewModel model)
        {
            var user = await _userRepository.GetByUserName(model.UserName);
            if (user == null)
                return null;

            var ingredient = await _ingredientRepository.GetByName(model.Ingredient.Name);
            if (ingredient == null)
                return null;

            var favorites = await _favoriteRepository.GetByUserFull(user);
            if (favorites == null)
                favorites = await Create(user);

            var exists = await _favoriteRepository.IngredientFavoriteExists(user, ingredient);
            if (exists)
            {
                favorites.Ingredients.Remove(ingredient);
                _logger.LogTrace("Removed Ingredient from Favorites!", favorites);
            }
            else
            {
                if (favorites.Ingredients == null)
                    favorites.Ingredients = new List<Ingredient>();

                favorites.Ingredients.Add(ingredient);
                _logger.LogTrace("Added Ingredient from Favorites!", favorites);
            }

            await _favoriteRepository.Update(favorites);

            _logger.LogTrace("Added Ingredient to Favorites!", favorites);

            return _mapper.Map<FavoritesResponse>(favorites);
        }
    }
}
