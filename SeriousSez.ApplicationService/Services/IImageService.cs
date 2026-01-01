using SeriousSez.Domain.Entities.Recipe;
using SeriousSez.Domain.Models;
using SeriousSez.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriousSez.ApplicationService.Services
{
    public interface IImageService
    {
        Task<Image> Create(ImageViewModel model);
        Task<Image> Delete(ImageResponse model);
        Task<IEnumerable<ImageResponse>> GetAll();
    }
}