using System.Collections.Generic;

namespace SeriousSez.Domain.Entities.Recipe
{
    public class Favorites : BaseEntity
    {
        public User User { get; set; }
        public List<Recipe> Recipes { get; set; }
        public List<Ingredient> Ingredients { get; set; }
    }
}
