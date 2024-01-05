using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwishBackend.StoreItems.Models;

namespace SwishBackend.StoreItems.Data
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.HasData(

                new ProductCategory
                {
                    Id = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                    CategoryName = "Painting"
                },

                new ProductCategory
                {
                    Id = new Guid("bb2ea796-c92a-4a02-8857-0763f7f4e80a"),
                    CategoryName = "Cars"
                });
        }
    }
}
