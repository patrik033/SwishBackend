using Microsoft.EntityFrameworkCore;
using SwishBackend.Orders.Data;


namespace SwishBackend.Orders
{
    public static class SeedData
    {
        public static void EnsureDataIsSeeded(WebApplication app)
        {
            using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<OrdersDbContext>();
            //context.Database.

            context.Database.Migrate();
        }
          
    }
}
