using SeriousSez.Domain.Entities.Recipe;
using SeriousSez.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using IngredientResponse = SeriousSez.Domain.Responses.IngredientResponse;

namespace SeriousSez.ApplicationService.Services
{
    public interface IIngredientService
    {
        Task<Ingredient> Create(IngredientViewModel model);
        Task<Ingredient> Get(IngredientResponse model);
        Task<Ingredient> Update(IngredientResponse model);
        Task<Ingredient> Delete(IngredientResponse model);
        Task<IEnumerable<IngredientResponse>> GetAll();
    }
}