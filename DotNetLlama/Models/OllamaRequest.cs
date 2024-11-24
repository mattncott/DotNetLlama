using System.Text.Json.Serialization;
using DotNetLlama.Enums;
using Newtonsoft.Json;

namespace DotNetLlama.Models
{
    public class OllamaRequest
    {
        [JsonPropertyName("model")]
        [JsonProperty("model")]
        public required string Model { get; set; }
        [JsonPropertyName("prompt")]
        [JsonProperty("prompt")]
        public required string Prompt{ get; set; }
        [JsonPropertyName("stream")]
        [JsonProperty("stream")]
        public bool Stream { get; set; } = false;

        // Currently as of 24th Nov 2024 only Format = JSON 
        // is supported so setting should be internal until further notice
        [JsonPropertyName("format")]
        [JsonProperty("format")]
        public OllamaFormat Format { get; internal set; }
    }
}