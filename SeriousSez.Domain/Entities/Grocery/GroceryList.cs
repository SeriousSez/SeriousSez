using System.Collections.Generic;

namespace SeriousSez.Domain.Entities.Grocery
{
    public class GroceryList : BaseEntity
    {
        public User User { get; set; }
        public List<GroceryIngredient> Ingredients { get; set; }
    }
}
