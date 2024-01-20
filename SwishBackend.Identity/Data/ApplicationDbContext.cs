using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SwishBackend.Identity.Models;

namespace SwishBackend.Identity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "3b570ab7-3b6f-4a2c-8b51-e4270cdf2d57",
                Name = Utility.StaticDetails.Role_Admin,
                NormalizedName = Utility.StaticDetails.Role_Admin.ToUpper(),
            });

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "45bf2ad4-7c42-46b0-8c66-55138bd38142",
                Name = Utility.StaticDetails.Role_User,
                NormalizedName = Utility.StaticDetails.Role_User.ToUpper(),
            });


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

            builder.Entity<ApplicationUser>().HasData(adminUser);
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "3b570ab7-3b6f-4a2c-8b51-e4270cdf2d57",
                UserId = "3a3e134a-2c3a-446f-86af-112d26fd2890"
            });

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

            builder.Entity<ApplicationUser>().HasData(customerUser);
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = "45bf2ad4-7c42-46b0-8c66-55138bd38142",
                UserId = "9a3e139a-1c7a-446f-86af-112d26fd2899"
            });

            base.OnModelCreating(builder);

        }

    }
}
