using SeriousSez.Domain.Entities.Fridge;
using SeriousSez.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriousSez.Infrastructure.Interfaces
{
    public interface IFridgeRepository : IBaseRepository<Fridge>
    {
        Task<ICollection<Fridge>> GetAllByUserId(Guid id);
    }
}
