using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SwishBackend.Identity.Models;

namespace SwishBackend.Identity.Data
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var adminUser = new ApplicationUser
            {
                Id = "3a3e134a-2c3a-446f-86af-112d26fd2890",
                Email = "admin.smiledentist@gmail.com",
                NormalizedEmail = "admin.smiledentist@gmail.com".ToUpper(),
                Name = "Admin",
                UserName = "admin.smiledentist@gmail.com",
                NormalizedUserName = "admin.smiledentist@gmail.com".ToUpper(),
                EmailConfirmed = true,
            };

            PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
            adminUser.PasswordHash = ph.HashPassword(adminUser, "Admin123#");


            var customerUser = new ApplicationUser
            {
                Id = "9a3e139a-1c7a-446f-86af-112d26fd2899",
                Email = "customer.smiledentist@gmail.com",
                NormalizedEmail = "customer.smiledentist@gmail.com".ToUpper(),
                Name = "Erik",
                UserName = "customer.smiledentist@gmail.com",
                NormalizedUserName = "customer.smiledentist@gmail.com".ToUpper(),
                EmailConfirmed = true,
            };

            PasswordHasher<ApplicationUser> ph2 = new PasswordHasher<ApplicationUser>();
            customerUser.PasswordHash = ph2.HashPassword(customerUser, "Customer123#");

            builder.HasData(adminUser, customerUser);
        }
    }
}
