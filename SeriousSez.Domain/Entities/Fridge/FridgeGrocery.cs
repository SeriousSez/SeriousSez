using SeriousSez.Domain.Entities.Recipe;
using System;

namespace SeriousSez.Domain.Entities.Fridge
{
    public class FridgeGrocery : BaseIngredient
    {
        public decimal Amount { get; set; }
        public string AmountType { get; set; }
        public DateTime PurchaseDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public Image Image { get; set; }
        public Guid FridgeId { get; set; }
    }
}
