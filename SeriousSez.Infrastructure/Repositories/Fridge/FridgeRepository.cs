using Microsoft.EntityFrameworkCore;
using SeriousSez.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SeriousSez.Infrastructure.Repositories.Fridge
{
    public class FridgeRepository : BaseRepository<Domain.Entities.Fridge.Fridge>, IFridgeRepository
    {
        protected internal SeriousContext _seriousContext { get { return _context as SeriousContext; } }

        public FridgeRepository(SeriousContext db) : base(db) { }

        public async Task<ICollection<Domain.Entities.Fridge.Fridge>> GetAllByUserId(Guid id)
        {
            return await _dbSet.Include(f => f.User)
                .Where(f => f.User.Id == id.ToString())
                .ToListAsync();
        }
    }
}
