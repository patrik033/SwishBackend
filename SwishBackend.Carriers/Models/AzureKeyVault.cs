using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

namespace SwishBackend.Carriers.Models
{
    public class AzureKeyVault
    {
        private bool _isInitialized = false;
        private readonly SecretClient _secretClient;

        private readonly string _emailApiKeySecretName;
        private string _emailSender;
        private string _emailApiKey;


        private  string _carrierDhlKey;
        private  string _carrierPostNordKey;

        public AzureKeyVault(IConfiguration configuration)
        {
            var keyVaultUri = configuration["KeyVault:Uri"];
            _secretClient = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());

            _carrierDhlKey = configuration["KeyVault:DHLKey"];
            _carrierPostNordKey = configuration["KeyVault:PostNordKey"];


            //_emailApiKeySecretName = configuration["KeyVault:EmailApiKeySecretName"];
            //_emailSender = configuration["KeyVault:Email"];


        }

        public async Task Initialize()
        {
            if (_isInitialized)
            {
                return;
            }

            var dhlSecret = await _secretClient.GetSecretAsync(_carrierDhlKey);
            var postNordSecret = await _secretClient.GetSecretAsync(_carrierPostNordKey);

            _carrierDhlKey = dhlSecret.Value.Value;
            _carrierPostNordKey = postNordSecret.Value.Value;

            //_emailApiKey = secretBundle.Value.Value;
            //_emailSender = emailBundle.Value.Value;
            _isInitialized = true;
        }

        public async Task<string> GetDhlSecretAsync()
        {
            await Initialize();
            return _carrierDhlKey;
        }

        public async Task<string> GetPostNordSecretAsync()
        {
            await Initialize();
            return _carrierPostNordKey;
        }
    }
}
