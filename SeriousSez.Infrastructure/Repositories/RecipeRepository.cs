using Microsoft.EntityFrameworkCore;
using SeriousSez.Domain.Entities;
using SeriousSez.Domain.Entities.Recipe;
using SeriousSez.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeriousSez.Infrastructure.Repositories
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly SeriousContext _context;

        public RecipeRepository(SeriousContext context)
        {
            _context = context;
        }

        public async Task Create(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task<Recipe> Get(Guid id)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id);
            return recipe;
        }

        public async Task<Recipe> GetFull(Guid id)
        {
            var recipe = await _context.Recipes.Include(r => r.RecipeIngredients).Include(r => r.Creator).FirstOrDefaultAsync(r => r.Id == id);
            return recipe;
        }

        public async Task<Recipe> GetByTitle(string title)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Title == title);
            return recipe;
        }

        public async Task<Recipe> GetByTitleFull(string title)
        {
            var recipe = await _context.Recipes
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)  // Only include Ingredient, not Recipe
                .Include(r => r.Creator)
                .Include(r => r.Image)
                .FirstOrDefaultAsync(r => r.Title == title);
            return recipe;
        }

        public async Task<Recipe> GetByTitleAndCreator(string title, string creator)
        {
            var recipe = await _context.Recipes.FirstOrDefaultAsync(r => r.Title == title && r.Creator.UserName == creator);

            return recipe;
        }

        public async Task<Recipe> GetByTitleAndCreatorFull(string title, string creator)
        {
            var recipe = await _context.Recipes
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)  // Only include Ingredient, not Recipe
                .Include(r => r.Creator)
                .Include(r => r.Image)
                .FirstOrDefaultAsync(r => r.Title == title && r.Creator.UserName == creator);

            return recipe;
        }

        public async Task<Recipe> GetByCreator(User user)
        {
            var recipe = await _context.Recipes.Include(r => r.Creator).FirstOrDefaultAsync(r => r.Creator == user);
            return recipe;
        }

        public async Task<IEnumerable<Recipe>> GetAll()
        {
            var recipes = await _context.Recipes.ToListAsync();
            return recipes;
        }

        public async Task<IEnumerable<Recipe>> GetAllByCreatorFull(string creator)
        {
            var recipes = await _context.Recipes
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)  // Only include Ingredient, not Recipe
                .Include(r => r.Creator)
                .Include(r => r.Image)
                .Where(r => r.Creator.UserName == creator)
                .ToListAsync();
            return recipes;
        }

        public async Task<IEnumerable<Recipe>> GetAllFull()
        {
            var recipes = await _context.Recipes
                .Include(r => r.RecipeIngredients)
                    .ThenInclude(ri => ri.Ingredient)  // Only include Ingredient, not Recipe
                .Include(r => r.Creator)
                .Include(r => r.Image)
                .ToListAsync();
            return recipes;
        }

        public async Task<IEnumerable<Recipe>> GetAllByIngredient(Guid recipeId)
        {
            var recipes = await _context.Recipes.Include(r => r.RecipeIngredients.Where(rI => rI.Ingredient.Id == recipeId)).Include(r => r.Creator).ToListAsync();
            return recipes;
        }

        public async Task Update(Recipe recipe)
        {
            _context.Recipes.Update(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Recipe recipe)
        {
            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRange(List<Recipe> recipes)
        {
            _context.Recipes.RemoveRange(recipes);
            await _context.SaveChangesAsync();
        }
    }
}