using System.Collections.Generic;

namespace SeriousSez.Domain.Entities.Plan
{
    public class GroceryPlan : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public List<Recipe.Recipe> Recipes { get; set; }
    }
}
