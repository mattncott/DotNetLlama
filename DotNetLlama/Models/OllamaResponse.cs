using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace DotNetLlama.Models
{
    public class OllamaResponse
    {
        [JsonPropertyName("model")]
        [JsonProperty("model")]
        public required string Model { get; set; }
        [JsonPropertyName("created_at")]
        [JsonProperty("created_at")]
        public required DateTime CreatedAt { get; set; }
        [JsonPropertyName("response")]
        [JsonProperty("response")]
        public required string Response { get; set; }
        [JsonPropertyName("done")]
        [JsonProperty("done")]
        public required bool Done { get; set; }
        [JsonPropertyName("done_reason")]
        [JsonProperty("done_reason")]
        public string? DoneReason { get; set; }
        [JsonPropertyName("context")]
        [JsonProperty("context")]
        public IEnumerable<int>? Context { get; set; }
    }
}