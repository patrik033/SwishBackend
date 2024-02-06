using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwishBackend.Identity.Models;

namespace SwishBackend.Identity.Data
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasData(
                new Address
                {
                    Id = 1,
                    City = "Eskilstuna",
                    ZipCode = "63352",
                    StreetAddress = "Idungatan",
                    StreetNumber = "1",
                    UserId = "3a3e134a-2c3a-446f-86af-112d26fd2890"
                },
                new Address
                {
                    Id = 2,
                    City = "Eskilstuna",
                    ZipCode = "63352",
                    StreetAddress = "Hödergatan",
                    StreetNumber = "2",
                    UserId = "9a3e139a-1c7a-446f-86af-112d26fd2899"
                });
        }
    }
}
