using System.Text.Json;

namespace lacuna_genetics
{
    public static class JsonExtensions
    {
        private static JsonSerializerOptions camelCaseSettings = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        };

        public static string Serialize<T>(T value)
        {
            return JsonSerializer.Serialize(value, options:camelCaseSettings);
        }

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, options:camelCaseSettings);
        }
    }
}
