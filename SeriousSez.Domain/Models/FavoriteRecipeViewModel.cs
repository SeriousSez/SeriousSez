using SeriousSez.Domain.Responses;

namespace SeriousSez.Domain.Models
{
    public class FavoriteRecipeViewModel
    {
        public string UserName { get; set; }
        public RecipeResponse Recipe { get; set; }
    }
}
