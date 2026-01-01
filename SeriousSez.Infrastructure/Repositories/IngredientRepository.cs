using Microsoft.EntityFrameworkCore;
using SeriousSez.Domain.Entities.Recipe;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriousSez.Infrastructure.Repositories
{
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
        protected internal SeriousContext _seriousContext { get { return _context as SeriousContext; } }

        public IngredientRepository(SeriousContext db) : base(db) { }

        public async Task<bool> Exists(string name)
        {
            return await _context.Ingredients.AnyAsync(i => i.Name == name);
        }

        public async Task<Ingredient> GetByName(string name)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(i => i.Name == name);
        }

        public async Task<Ingredient> GetByNameFull(string name)
        {
            return await _context.Ingredients.Include(i => i.Image).FirstOrDefaultAsync(i => i.Name == name);
        }

        public async Task<IEnumerable<Ingredient>> GetAllFull()
        {
            return await _context.Ingredients.Include(i => i.Image).ToListAsync();
        }
    }
}
