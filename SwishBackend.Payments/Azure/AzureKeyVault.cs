using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace SwishBackend.Payments.Models.Azure
{
    public class AzureKeyVault
    {
        private bool _isInitialized = false;
        private readonly SecretClient _secretClient;
        private  string _stripeKey;


        public AzureKeyVault(IConfiguration configuration)
        {
            var keyVaultUri = configuration["KeyVault:Uri"];
            _stripeKey = configuration["KeyVault:Stripe"];
          
            _secretClient = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());


        }

        public async Task Initialize()
        {
            if (_isInitialized)
            {
                return;
            }

            var secretBundle = await _secretClient.GetSecretAsync(_stripeKey);


            _stripeKey = secretBundle.Value.Value;
            _isInitialized = true;
        }

        public async Task<string> GetStripeSecretAsync()
        {
            await Initialize();
            return _stripeKey;
        }

    }
}
