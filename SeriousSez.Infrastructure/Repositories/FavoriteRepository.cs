using Microsoft.EntityFrameworkCore;
using SeriousSez.Domain.Entities;
using SeriousSez.Domain.Entities.Recipe;
using System.Linq;
using System.Threading.Tasks;

namespace SeriousSez.Infrastructure.Repositories
{
    public class FavoriteRepository : BaseRepository<Favorites>, IFavoriteRepository
    {
        protected internal SeriousContext _seriousContext { get { return _context as SeriousContext; } }

        public FavoriteRepository(SeriousContext db) : base(db) { }

        public async Task<bool> RecipeFavoriteExists(User user, Recipe recipe)
        {
            return await _context.Favorites.Include(f => f.Recipes).AnyAsync(f => f.User == user && f.Recipes.Any(r => r == recipe));
        }

        public async Task<bool> IngredientFavoriteExists(User user, Ingredient ingredient)
        {
            return await _context.Favorites.Include(f => f.Ingredients).AnyAsync(f => f.User == user && f.Ingredients.Any(i => i == ingredient));
        }

        public async Task<Favorites> GetByUser(User user)
        {
            return await _context.Favorites.FirstOrDefaultAsync(f => f.User == user);
        }

        public async Task<Favorites> GetByUserFull(User user)
        {
            return await _context.Favorites.Include(f => f.User).Include(f => f.Recipes).ThenInclude(r => r.Creator).Include(f => f.Recipes).ThenInclude(r => r.Image).Include(f => f.Ingredients).FirstOrDefaultAsync(f => f.User == user);
        }
    }
}
