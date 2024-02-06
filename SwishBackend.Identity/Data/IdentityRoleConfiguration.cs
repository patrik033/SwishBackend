using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SwishBackend.Identity.Data
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "3b570ab7-3b6f-4a2c-8b51-e4270cdf2d57",
                    Name = Utility.StaticDetails.Role_Admin,
                    NormalizedName = Utility.StaticDetails.Role_Admin.ToUpper(),
                },
                new IdentityRole
                {
                    Id = "45bf2ad4-7c42-46b0-8c66-55138bd38142",
                    Name = Utility.StaticDetails.Role_User,
                    NormalizedName = Utility.StaticDetails.Role_User.ToUpper(),
                });
        }
    }
}
