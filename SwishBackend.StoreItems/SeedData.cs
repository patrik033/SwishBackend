using Microsoft.EntityFrameworkCore;
using SwishBackend.StoreItems.Data;

namespace SwishBackend.StoreItems
{
    public static class SeedData
    {
        public static void EnsureDataIsSeeded(WebApplication app)
        {
            using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<StoreItemsDbContext>();

            if (!context.ProductCategories.Any() || !context.ProductCategories.Any())
            {
                // If no data exists, seed the database
                SeedDatabase(context);
            }
            else
            {
                // Check if the current migration is newer than what the database has
                var appliedMigrations = context.Database.GetAppliedMigrations();
                var pendingMigrations = context.Database.GetPendingMigrations();

                if (pendingMigrations.Any())
                {
                    // There are pending migrations, apply them
                    context.Database.Migrate();
                }
                else
                {
                    // Database is up-to-date
                    Console.WriteLine("Database is up-to-date.");
                }
            }
        }

        private static void SeedDatabase(StoreItemsDbContext context)
        {
            // Your seeding logic here
            // For example:
           
            context.SaveChanges();
        }
    }
}
