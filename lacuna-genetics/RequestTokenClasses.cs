using System.Text.Json.Serialization;

namespace lacuna_genetics
{
    public class RequestTokenBody
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("password")]
        public string Password { get; set; }

        public RequestTokenBody() { }
    }

    public class RequestTokenResponse
    {
        [JsonPropertyName("accessToken")]
        public string? AccessToken { get; set; }
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }

        public RequestTokenResponse() { }
    }
}
