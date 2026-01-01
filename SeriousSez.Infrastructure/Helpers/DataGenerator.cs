using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SeriousSez.Domain.Entities;
using System;
using System.Linq;

namespace SeriousSez.Infrastructure.Helpers
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            //using (var context = new SeriousContext(serviceProvider.GetRequiredService<DbContextOptions<SeriousContext>>()))
            //{
            //    // Look for any board games.
            //    if (context.Users.Any())
            //    {
            //        return;   // Data was already seeded
            //    }

            //    context.Users.AddRange(
            //        new User
            //        {

            //        });

            //    context.SaveChanges();
            //}
        }
    }
}
