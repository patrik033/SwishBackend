using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwishBackend.StoreItems.Models;

namespace SwishBackend.StoreItems.Data
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.Property(p => p.Price).HasPrecision(18, 4);
        }
    }
}
