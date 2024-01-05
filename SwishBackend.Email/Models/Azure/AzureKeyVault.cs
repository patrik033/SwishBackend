
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Azure.KeyVault;
using Microsoft.Extensions.Options;



namespace SwishBackend.Email.Models.Azure
{
    public class AzureKeyVault
    {
        private bool _isInitialized = false;
        private readonly SecretClient _secretClient;
        private readonly string _emailApiKeySecretName;
        private  string _emailSender;
        private string _emailApiKey;

        public AzureKeyVault(IConfiguration configuration)
        {
            var keyVaultUri = configuration["KeyVault:Uri"];
            _emailApiKeySecretName = configuration["KeyVault:EmailApiKeySecretName"];
            _emailSender = configuration["KeyVault:Email"];
            _secretClient = new SecretClient(new Uri(keyVaultUri), new DefaultAzureCredential());
           
           
        }

        public async Task Initialize()
        {
            if (_isInitialized)
            {
                return;
            }

            var secretBundle = await _secretClient.GetSecretAsync(_emailApiKeySecretName);
            var emailBundle = await _secretClient.GetSecretAsync(_emailSender);

            _emailApiKey = secretBundle.Value.Value;
            _emailSender = emailBundle.Value.Value;
            _isInitialized = true;
        }

        public async Task<string> GetSendgridSecretAsync()
        {
            await Initialize();
            return _emailApiKey;
        }

        public async Task<string> GetSenderEmailSecretAsync()
        {
            await Initialize();
            return _emailSender;
        }
    }
}
