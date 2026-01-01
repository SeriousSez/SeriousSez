using SeriousSez.Domain.Entities.Recipe;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriousSez.Infrastructure.Repositories
{
    public interface IIngredientRepository : IBaseRepository<Ingredient>
    {
        Task<bool> Exists(string name);
        Task<Ingredient> GetByName(string name);
        Task<Ingredient> GetByNameFull(string name);
        Task<IEnumerable<Ingredient>> GetAllFull();
    }
}