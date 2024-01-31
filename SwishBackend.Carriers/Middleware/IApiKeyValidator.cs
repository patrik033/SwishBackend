namespace SwishBackend.Carriers.Middleware
{
    public interface IApiKeyValidator
    {
        bool IsValid(string apiKey);
    }
}

