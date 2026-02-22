using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SeriousSez.ApplicationService.Services;
using SeriousSez.Domain.Models;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace SeriousSez.Api.Services
{
    public class WikipediaIngredientImageGenerator : IIngredientImageGenerator
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<WikipediaIngredientImageGenerator> _logger;

        public WikipediaIngredientImageGenerator(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<WikipediaIngredientImageGenerator> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<ImageViewModel> GenerateAsync(string ingredientName, string description)
        {
            var section = _configuration.GetSection("AIImageGeneration");
            var enabled = bool.TryParse(section["Enabled"], out var parsedEnabled) && parsedEnabled;
            if (!enabled)
            {
                return null;
            }

            var safeIngredientName = string.IsNullOrWhiteSpace(ingredientName) ? "ingredient" : ingredientName.Trim();
            var pageTitle = WebUtility.UrlEncode(safeIngredientName.Replace(' ', '_'));

            using var request = new HttpRequestMessage(HttpMethod.Get, $"https://en.wikipedia.org/api/rest_v1/page/summary/{pageTitle}");
            request.Headers.TryAddWithoutValidation("User-Agent", "SeriousSez/1.0 (ingredient-image-generator)");

            using var response = await _httpClient.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                _logger.LogInformation("No free Wikipedia image found for ingredient {IngredientName}. Status: {StatusCode}", safeIngredientName, (int)response.StatusCode);
                return null;
            }

            using var stream = await response.Content.ReadAsStreamAsync();
            using var json = await JsonDocument.ParseAsync(stream);

            string imageUrl = null;
            if (json.RootElement.TryGetProperty("thumbnail", out var thumbnail) &&
                thumbnail.TryGetProperty("source", out var source))
            {
                imageUrl = source.GetString();
            }

            if (string.IsNullOrWhiteSpace(imageUrl) &&
                json.RootElement.TryGetProperty("originalimage", out var originalImage) &&
                originalImage.TryGetProperty("source", out var originalSource))
            {
                imageUrl = originalSource.GetString();
            }

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return null;
            }

            return new ImageViewModel
            {
                Url = imageUrl,
                Caption = $"Image for {safeIngredientName} (Wikipedia)"
            };
        }
    }
}