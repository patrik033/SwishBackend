namespace SwishBackend.Carriers.Middleware
{
    public class ApiKeyMiddleware : IApiKeyValidator
    {
        private readonly RequestDelegate _next;
        private const string ApiKeyName = "X-Api-Key";

        public ApiKeyMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var apiKey = context.Request.Headers[ApiKeyName];

            if (!IsValid(apiKey))
            {
                context.Response.StatusCode = 401; // Unauthorized
                await context.Response.WriteAsync("Invalid API Key");
                return;
            }

            await _next(context);
        }

        public bool IsValid(string apiKey)
        {
            // Validate apiKey against a storage mechanism (e.g., database)
            // Return true if apiKey is valid, false otherwise
            // You may fetch and compare apiKey from a database, configuration, or any other storage
            // This is a simplified example
            return apiKey == "YourSecretApiKey";
        }
    }
}
