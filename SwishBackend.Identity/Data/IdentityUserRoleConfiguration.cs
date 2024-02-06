using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SwishBackend.Identity.Data
{
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
            new IdentityUserRole<string>
            {
                RoleId = "3b570ab7-3b6f-4a2c-8b51-e4270cdf2d57",
                UserId = "3a3e134a-2c3a-446f-86af-112d26fd2890"
            },
            new IdentityUserRole<string>
            {
                RoleId = "45bf2ad4-7c42-46b0-8c66-55138bd38142",
                UserId = "9a3e139a-1c7a-446f-86af-112d26fd2899"
            });
        }
    }
}
