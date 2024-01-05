using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwishBackend.StoreItems.Models;

namespace SwishBackend.StoreItems.Data
{
    public class CarConfigurations : IEntityTypeConfiguration<Car>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasData(

                new Car
                {
                    Id = Guid.NewGuid(),
                    Name = "Car 1",
                    Price = 30000M,
                    Image = "car_image_1.jpg",
                    Stock = 5,
                    HorsePower = 200,
                    Model = "Model X",
                    ProductCategoryId = new Guid("bb2ea796-c92a-4a02-8857-0763f7f4e80a")// Cars category
                },
                new Car
                {
                    Id = Guid.NewGuid(),
                    Name = "Car 2",
                    Price = 25000M,
                    Image = "car_image_2.jpg",
                    Stock = 8,
                    HorsePower = 150,
                    Model = "Model Y",
                    ProductCategoryId = new Guid("bb2ea796-c92a-4a02-8857-0763f7f4e80a") // Cars category
                });


        }
    }
}


