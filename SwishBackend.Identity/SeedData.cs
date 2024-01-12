using Microsoft.EntityFrameworkCore;
using SwishBackend.Identity.Data;


namespace SwishBackend.Identity
{
    public static class SeedData
    {
        public static void EnsureDataIsSeeded(WebApplication app)
        {
            using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.Migrate();
          
        }
    }
}
