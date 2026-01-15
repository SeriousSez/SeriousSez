using SeriousSez.Domain.Entities.Recipe;
using SeriousSez.Domain.Models;
using SeriousSez.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriousSez.ApplicationService.Services
{
    public interface IRecipeService
    {
        Task<RecipeResponse> Create(RecipeViewModel model);
        Task<RecipeResponse> AddIngredients(List<IngredientResponse> ingredients, string title, string creator);
        Task<Recipe> Get(RecipeResponse model);
        Task<RecipeResponse> Get(string title, string creator);
        Task<IEnumerable<RecipeResponse>> GetAll();
        Task<IEnumerable<RecipeResponse>> GetAll(string creator);
        Task<List<RecipeResponse>> GetAllByIngredient(IngredientResponse model);
        Task<RecipeResponse> Update(RecipeUpdateViewModel model);
        Task<bool> DeleteRecipeIngredient(IngredientResponse model);
        Task<Recipe> Delete(Guid id);
    }
}