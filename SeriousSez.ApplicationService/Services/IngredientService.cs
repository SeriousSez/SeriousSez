using AutoMapper;
using Microsoft.Extensions.Logging;
using SeriousSez.Domain.Entities.Recipe;
using SeriousSez.Domain.Models;
using SeriousSez.Infrastructure.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using SeriousSez.Domain.Responses;
using System;

namespace SeriousSez.ApplicationService.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly ILogger<IngredientService> _logger;
        private readonly IIngredientRepository _ingredientRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IIngredientImageGenerator _ingredientImageGenerator;
        private readonly IMapper _mapper;

        public IngredientService(
            ILogger<IngredientService> logger,
            IIngredientRepository ingredientRepository,
            IImageRepository imageRepository,
            IIngredientImageGenerator ingredientImageGenerator,
            IMapper mapper)
        {
            _logger = logger;
            _ingredientRepository = ingredientRepository;
            _imageRepository = imageRepository;
            _ingredientImageGenerator = ingredientImageGenerator;
            _mapper = mapper;
        }

        public async Task<Ingredient> Create(IngredientViewModel model)
        {
            if (model.Image == null || string.IsNullOrWhiteSpace(model.Image.Url) || IsPlaceholderImageUrl(model.Image.Url))
            {
                model.Image = null;
                var generatedImage = await _ingredientImageGenerator.GenerateAsync(model.Name, model.Description);
                if (generatedImage != null)
                {
                    model.Image = generatedImage;
                }
            }

            var ingredient = _mapper.Map<Ingredient>(model);
            await _ingredientRepository.Create(ingredient);

            _logger.LogTrace("Ingredient created! Ingredient: {@Ingredient}", ingredient);

            return ingredient;
        }

        public async Task<Ingredient> Get(IngredientResponse model)
        {
            var ingredient = await _ingredientRepository.GetByName(model.Name);
            await _ingredientRepository.Create(ingredient);

            _logger.LogTrace("Ingredient created! Ingredient: {@Ingredient}", ingredient);

            return ingredient;
        }

        public async Task<IEnumerable<IngredientResponse>> GetAll()
        {
            var ingredients = await _ingredientRepository.GetAllFull();

            var ingredientList = new List<IngredientResponse>();
            foreach (var ingredient in ingredients)
            {
                var ingredientResponse = _mapper.Map<IngredientResponse>(ingredient);
                ingredientList.Add(ingredientResponse);
            }

            return ingredientList;
        }

        public async Task<Ingredient> Update(IngredientResponse model)
        {
            var ingredient = await _ingredientRepository.GetByName(model.Name);
            ingredient.Name = model.Name;
            ingredient.Description = model.Description;

            var image = await _imageRepository.GetByUrl(model.Image.Url);
            if (image == null)
            {
                await _imageRepository.Delete(ingredient.Image);
                var newImage = _mapper.Map<Image>(model);
                await _imageRepository.Create(newImage);
            }

            await _ingredientRepository.Update(ingredient);

            _logger.LogTrace("Ingredient updated! Ingredient: {@Ingredient}", ingredient);

            return ingredient;
        }

        public async Task<Ingredient> Delete(IngredientResponse model)
        {
            var ingredient = await _ingredientRepository.GetByNameFull(model.Name);
            await _imageRepository.Delete(ingredient.Image);
            await _ingredientRepository.Delete(ingredient);

            _logger.LogTrace("Ingredient deleted! Ingredient: {@Ingredient}", ingredient);

            return ingredient;
        }

        private static bool IsPlaceholderImageUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return true;
            }

            var normalized = url.Replace("\\", "/").ToLowerInvariant();
            return normalized.Contains("/assets/images/");
        }
    }
}
