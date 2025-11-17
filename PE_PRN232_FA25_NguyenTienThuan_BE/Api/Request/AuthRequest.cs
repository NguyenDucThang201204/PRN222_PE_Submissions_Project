using System.Text.Json.Serialization;

namespace Api.Request
{
    public class AuthRequest
    {
        [JsonPropertyName("userName")]
        public string UserName { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
