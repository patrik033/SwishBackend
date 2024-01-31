using SwishBackend.Carriers.Models;

namespace SwishBackend.Carriers.Extensions
{
    public static class ServiceExtensions
    {
        public static async Task ConfigureAzure(this IServiceCollection services, IConfiguration configuration)
        {
            var azureKeyVault = new AzureKeyVault(configuration);
            await azureKeyVault.Initialize();
            services.AddSingleton(azureKeyVault);
        }
    }
}
