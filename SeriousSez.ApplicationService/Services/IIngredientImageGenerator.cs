using SeriousSez.Domain.Models;
using System.Threading.Tasks;

namespace SeriousSez.ApplicationService.Services
{
    public interface IIngredientImageGenerator
    {
        Task<ImageViewModel> GenerateAsync(string ingredientName, string description);
    }
}
