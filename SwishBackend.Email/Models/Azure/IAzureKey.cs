namespace SwishBackend.Email.Models.Azure
{
    public interface IAzureKey
    {
        Task Initialize(string keyVaultUrl);
    }
}
