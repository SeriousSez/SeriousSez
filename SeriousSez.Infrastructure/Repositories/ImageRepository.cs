using Microsoft.EntityFrameworkCore;
using SeriousSez.Domain.Entities.Recipe;
using System.Threading.Tasks;

namespace SeriousSez.Infrastructure.Repositories
{
    public class ImageRepository : BaseRepository<Image>, IImageRepository
    {
        protected internal SeriousContext _seriousContext { get { return _context as SeriousContext; } }

        public ImageRepository(SeriousContext db) : base(db) { }

        public async Task<Image> GetByUrl(string url)
        {
            return await _context.Images.FirstOrDefaultAsync(i => i.Url == url);
        }
    }
}
