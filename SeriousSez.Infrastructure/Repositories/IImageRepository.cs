using SeriousSez.Domain.Entities.Recipe;
using System.Threading.Tasks;

namespace SeriousSez.Infrastructure.Repositories
{
    public interface IImageRepository : IBaseRepository<Image>
    {
        Task<Image> GetByUrl(string url);
    }
}