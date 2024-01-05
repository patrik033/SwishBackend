using Microsoft.AspNetCore.Identity;

namespace SwishBackend.Identity.Email.Token
{
    public class EmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
    {
        public EmailConfirmationTokenProviderOptions()
        {
            Name = "EmailDataProtectorTokenProvider";
            TokenLifespan = TimeSpan.FromMinutes(60);
        }
    }
}
