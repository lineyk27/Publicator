using System.Text.Json;
using System.Text.Json.Serialization;

namespace Publicator.Core.Domains.User.Commands
{
    public class FacebookAppAccessToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }
        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

    }
}
