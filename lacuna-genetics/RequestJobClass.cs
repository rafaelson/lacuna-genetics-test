using System.Text.Json.Serialization;

namespace lacuna_genetics
{
    public class JobResponse
    {
        [JsonPropertyName("job")]
        public class Job
        {
            [JsonPropertyName("id")]
            public string Id { get; set; }
            [JsonPropertyName("type")]
            public string Type { get; set; }
            [JsonPropertyName("strand")]
            public string? Strand { get; set; }
            [JsonPropertyName("strandEncoded")]
            public string? StrandEncoded { get; set; }
            [JsonPropertyName("geneEncoded")]
            public string? GeneEncoded { get; set; }

        }
        [JsonPropertyName("code")]
        public string Code { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }

    }
}
