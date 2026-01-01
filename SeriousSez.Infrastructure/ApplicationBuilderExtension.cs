using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace SeriousSez.Infrastructure
{
    public static class ApplicationBuilderExtension
    {
		public static void SeedDatabase(this IApplicationBuilder app)
		{
			using var serviceScope = app.ApplicationServices.CreateScope();
			var context = serviceScope.ServiceProvider.GetRequiredService<SeriousContext>();

			SeedRecipes(context);
			SeedUserSettings(context);

			context.SaveChanges();
		}

		private static void SeedRecipes(SeriousContext context)
		{
			var recipes = context.Recipes.Where(r => string.IsNullOrWhiteSpace(r.Description) && string.IsNullOrWhiteSpace(r.Language)).ToList();

			foreach(var recipe in recipes)
            {
				recipe.Description = "This recipe has not added a description yet!";
				recipe.Language = "English";
            }

			context.UpdateRange(recipes);
		}

		private static void SeedUserSettings(SeriousContext context)
		{
			var userSettings = context.UserSettings.Where(r => string.IsNullOrWhiteSpace(r.PreferredLanguage)).ToList();

			foreach (var settings in userSettings)
			{
				settings.PreferredLanguage = "English";
			}

			context.UpdateRange(userSettings);
		}
	}
}
