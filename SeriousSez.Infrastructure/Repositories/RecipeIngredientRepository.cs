using Microsoft.EntityFrameworkCore;
using SeriousSez.Domain.Entities.Recipe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeriousSez.Infrastructure.Repositories
{
    public class RecipeIngredientRepository : BaseRepository<RecipeIngredient>, IRecipeIngredientRepository
    {
        protected internal SeriousContext _seriousContext { get { return _context as SeriousContext; } }

        public RecipeIngredientRepository(SeriousContext db) : base(db) { }

        public async Task<RecipeIngredient> GetFull(Guid id)
        {
            var recipeIngredient = await _context.RecipeIngredients.Include(r => r.Ingredient).Include(r => r.Recipe).FirstOrDefaultAsync(r => r.Id == id);
            return recipeIngredient;
        }

        public async Task<RecipeIngredient> GetFullByIngredient(Ingredient ingredient)
        {
            var recipeIngredient = await _context.RecipeIngredients.Include(r => r.Ingredient).Include(r => r.Recipe).FirstOrDefaultAsync(r => r.Ingredient == ingredient);
            return recipeIngredient;
        }

        public async Task<RecipeIngredient> GetFullByRecipe(Recipe recipe)
        {
            var recipeIngredient = await _context.RecipeIngredients.Include(r => r.Ingredient).Include(r => r.Recipe).FirstOrDefaultAsync(r => r.Recipe == recipe);
            return recipeIngredient;
        }

        public async Task<List<RecipeIngredient>> GetAllByRecipe(Recipe recipe)
        {
            var recipeIngredients = await _context.RecipeIngredients.Include(r => r.Ingredient).Include(r => r.Recipe).Where(r => r.Recipe == recipe).ToListAsync();
            return recipeIngredients;
        }
    }
}
