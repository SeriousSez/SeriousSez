using Microsoft.EntityFrameworkCore;
using SeriousSez.Domain.Entities.Fridge;
using SeriousSez.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeriousSez.Infrastructure.Repositories.Fridge
{
    public class FridgeGroceryRepository : BaseRepository<FridgeGrocery>, IFridgeGroceryRepository
    {
        protected internal SeriousContext _seriousContext { get { return _context as SeriousContext; } }

        public FridgeGroceryRepository(SeriousContext db) : base(db) { }

        public async Task<ICollection<FridgeGrocery>> GetByFridgeId(Guid id)
        {
            return await _dbSet.Where(f => f.FridgeId == id).ToListAsync();
        }
    }
}
