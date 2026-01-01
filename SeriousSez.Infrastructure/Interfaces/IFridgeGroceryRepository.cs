using SeriousSez.Domain.Entities.Fridge;
using SeriousSez.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriousSez.Infrastructure.Interfaces
{
    public interface IFridgeGroceryRepository : IBaseRepository<FridgeGrocery>
    {
        Task<ICollection<FridgeGrocery>> GetByFridgeId(Guid id);
    }
}
