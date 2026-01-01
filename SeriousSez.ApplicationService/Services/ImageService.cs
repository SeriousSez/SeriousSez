using AutoMapper;
using Microsoft.Extensions.Logging;
using SeriousSez.Domain.Entities.Recipe;
using SeriousSez.Domain.Models;
using SeriousSez.Domain.Responses;
using SeriousSez.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeriousSez.ApplicationService.Services
{
    public class ImageService : IImageService
    {
        private readonly ILogger<ImageService> _logger;
        private readonly IImageRepository _imageRepository;
        private readonly IMapper _mapper;

        public ImageService(ILogger<ImageService> logger, IImageRepository imageRepository, IMapper mapper)
        {
            _logger = logger;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }

        public async Task<Image> Create(ImageViewModel model)
        {
            var ingredient = _mapper.Map<Image>(model);
            await _imageRepository.Create(ingredient);

            _logger.LogTrace("Ingredient created!", ingredient);

            return ingredient;
        }

        public async Task<Image> Delete(ImageResponse model)
        {
            var image = await _imageRepository.Get(model.Id);
            await _imageRepository.Delete(image);

            _logger.LogTrace("Ingredient deleted!", image);

            return image;
        }

        public async Task<IEnumerable<ImageResponse>> GetAll()
        {
            var images = await _imageRepository.GetAll();

            var imageList = new List<ImageResponse>();
            foreach (var image in images)
            {
                var imageResponse = _mapper.Map<ImageResponse>(image);
                imageList.Add(imageResponse);
            }

            return imageList;
        }
    }
}
