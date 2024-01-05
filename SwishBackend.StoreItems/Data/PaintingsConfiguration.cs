using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwishBackend.StoreItems.Models;

namespace SwishBackend.StoreItems.Data
{
    public class PaintingsConfiguration : IEntityTypeConfiguration<Painting>
    {
        public void Configure(EntityTypeBuilder<Painting> builder)
        {


            builder.HasData(

             new Painting
             {
                 Id = Guid.NewGuid(),
                 Name = "Test Painging 1",
                 Price = 100M,
                 Image = "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
                 Stock = 10,
                 ProductCategoryId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                 PainterName = "Test 1"
             },
             new Painting
             {
                 Id = Guid.NewGuid(),
                 Name = "Test Painging 2",
                 Price = 110M,
                 Image = "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
                 Stock = 16,
                 ProductCategoryId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                 PainterName = "Test 2"


             },
             new Painting
             {
                 Id = Guid.NewGuid(),
                 Name = "Test Painging 3",
                 Price = 120M,
                 Image = "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
                 Stock = 12,
                 ProductCategoryId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                 PainterName = "Test 3"


             },
             new Painting
             {
                 Id = Guid.NewGuid(),
                 Name = "Test Painging 4",
                 Price = 100M,
                 Image = "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
                 Stock = 5,
                 ProductCategoryId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                 PainterName = "Test 4"


             },
             new Painting
             {
                 Id = Guid.NewGuid(),
                 Name = "Test Painging 5",
                 Price = 200M,
                 Image = "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
                 Stock = 12,
                 ProductCategoryId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                 PainterName = "Test 5"


             },
             new Painting
             {
                 Id = Guid.NewGuid(),
                 Name = "Test Painging 6",
                 Price = 100M,
                 Image = "https://images.pexels.com/photos/1266808/pexels-photo-1266808.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=2",
                 Stock = 10,
                 ProductCategoryId = new Guid("0f8fad5b-d9cb-469f-a165-70867728950e"),
                 PainterName = "Test 6"
             });
        }
    }
}
