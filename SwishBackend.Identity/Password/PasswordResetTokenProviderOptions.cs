using Microsoft.AspNetCore.Identity;

namespace SwishBackend.Identity.Password
{
    public class PasswordResetTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public PasswordResetTokenProviderOptions()
        {
            Name = "PasswordResetTokenProvider";
            TokenLifespan = TimeSpan.FromMinutes(5);
        }
    }
}
