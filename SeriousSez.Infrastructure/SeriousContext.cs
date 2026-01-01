using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SeriousSez.Domain.Entities;
using SeriousSez.Domain.Entities.Fridge;
using SeriousSez.Domain.Entities.Grocery;
using SeriousSez.Domain.Entities.Plan;
using SeriousSez.Domain.Entities.Recipe;

namespace SeriousSez.Infrastructure
{
    public class SeriousContext : IdentityDbContext<User>
    {
        public IConfiguration Configuration { get; }

        public SeriousContext()
        {

        }

        public SeriousContext(DbContextOptions<SeriousContext> options) : base(options)
        {
        }

        public SeriousContext(DbContextOptions<SeriousContext> options, IConfiguration configuration) : base(options)
        {
            Configuration = configuration;
        }

        public DbSet<UserSeeker> UserSeekers { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }

        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Favorites> Favorites { get; set; }

        public DbSet<GroceryPlan> GroceryPlans { get; set; }

        public DbSet<GroceryList> GroceryLists { get; set; }
        public DbSet<GroceryIngredient> GroceryIngredients { get; set; }

        public DbSet<Fridge> Fridges { get; set; }
        public DbSet<FridgeGrocery> FridgeGroceries { get; set; }

        public DbSet<Image> Images { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)½                                   
        //{
        //    string adminId = "02174cf0–9412–-afbf-59f706d72cf6";
        //    string roleId = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";

        //    //seed admin role
        //    modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
        //    {
        //        Name = "Admin",
        //        NormalizedName = "ADMIN",
        //    });

        //    //create user
        //    var user = new User 
        //    { 
        //        Id = Guid.NewGuid().ToString(), 
        //        UserName = "Admin",
        //        NormalizedUserName = "ADMIN",
        //        Firstname = "Admin", 
        //        Lastname = "",
        //        Email = "serious@sezginsahin.dk",
        //        EmailConfirmed = true,
        //        PasswordHash = "471e6604ad6b4f9b85a81305feefb4f7" 
        //    };

        //    //set user password
        //    PasswordHasher<User> ph = new PasswordHasher<User>();
        //    user.PasswordHash = ph.HashPassword(user, "Admin1!");

        //    //seed user
        //    modelBuilder.Entity<User>().HasData(user);

        //    //set user role to admin
        //    modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        //    {
        //        RoleId = roleId,
        //        UserId = adminId
        //    });
        //}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#if DEBUG
                optionsBuilder.UseInMemoryDatabase(databaseName: "SeriousSez");
#else
                optionsBuilder.UseMySql(Configuration.GetConnectionString("MySql"), new MySqlServerVersion(new Version(8, 0, 11)));
#endif
            }
        }
    }
}
