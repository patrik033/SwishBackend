using Newtonsoft.Json;

namespace SwishBackend.Identity.Email.Token
{
    public class TokenTemplateData
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("token")]
        public string Token { get; set; }
    }
}
