using SeriousSez.Domain.Responses;
using System;
using System.Threading.Tasks;

namespace SeriousSez.ApplicationService.Interfaces
{
    public interface IGroceryService
    {
        Task<GroceryListResponse> GetGroceryList(string userId);
        Task Create(Guid userId);
    }
}
