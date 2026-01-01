using SeriousSez.Domain.Responses;

namespace SeriousSez.Domain.Models
{
    public class FavoriteIngredientViewModel
    {
        public string UserName { get; set; }
        public IngredientResponse Ingredient { get; set; }
    }
}
