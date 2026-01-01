using SeriousSez.Domain.Entities;
using SeriousSez.Domain.Models;
using SeriousSez.Domain.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriousSez.ApplicationService.Interfaces
{
    public interface IFridgeService
    {
        Task<ICollection<FridgeResponse>> Get(Guid userId);
        Task Add(FridgeModel model);
        Task AddHomeFridge(User user);
        Task Retire(Guid fridgeId);
        Task UnRetire(Guid fridgeId);
        Task<ICollection<FridgeGroceryResponse>> GetGroceries(Guid fridgeId);
        Task AddGrocery(FridgeGroceryModel model);
        Task RemoveGrocery(Guid id);
    }
}
