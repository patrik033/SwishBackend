using Microsoft.EntityFrameworkCore;
using SwishBackend.StoreItems.Models;

namespace SwishBackend.StoreItems.Data
{
    public class StoreItemsDbContext : DbContext
    {


        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

  
        public StoreItemsDbContext(DbContextOptions<StoreItemsDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            


            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new PaintingsConfiguration());
            modelBuilder.ApplyConfiguration(new CarConfigurations());


            base.OnModelCreating(modelBuilder);
        }
    }
}
