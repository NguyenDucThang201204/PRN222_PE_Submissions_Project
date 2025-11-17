

using System.Text.Json.Serialization;

namespace Api.Response
{
    public class AuthReponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("role")]
        public string Role { get; set; }
    }
}
