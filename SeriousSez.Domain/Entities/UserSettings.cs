namespace SeriousSez.Domain.Entities
{
    public class UserSettings : BaseEntity
    {
        public string PreferredLanguage { get; set; } = "English";
        public string Theme { get; set; }
        public string RecipesTheme { get; set; }
        public string MyRecipesTheme { get; set; }
        //public string IngredientsTheme { get; set; }
        public User Identity { get; set; }
    }
}
