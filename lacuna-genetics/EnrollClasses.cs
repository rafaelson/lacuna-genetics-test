using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace lacuna_genetics
{
    public class EnrollBody
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }
        [JsonPropertyName("email")]

        public string Email { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        public EnrollBody() { }
    }

    public class EnrollResponse
    {
        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("message")]
        public string? Message { get; set; }

        public EnrollResponse() { }
    }
}
