using SeriousSez.Domain.Entities.Grocery;
using System;
using System.Collections.Generic;

namespace SeriousSez.Domain.Entities.Fridge
{
    public class Fridge : BaseEntity
    {
        public string Name { get; set; }
        public DateTime Purchased { get; set; }
        public DateTime Retired { get; set; } = DateTime.MinValue;
        public ICollection<FridgeGrocery> Groceries { get; set; }
        public User User { get; set; }
    }
}
