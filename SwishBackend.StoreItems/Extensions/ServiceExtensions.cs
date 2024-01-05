using Microsoft.EntityFrameworkCore;
using SwishBackend.StoreItems.Data;

namespace SwishBackend.StoreItems.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureSqlContext(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<StoreItemsDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();
                    builder.WithExposedHeaders("X-Pagination");
                });
            });
        }
    }
}
