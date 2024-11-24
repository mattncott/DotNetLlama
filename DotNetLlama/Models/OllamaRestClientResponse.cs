namespace DotNetLlama.Models
{
    public class OllamaRestClientResponse
    {
        public required bool Success { get; set; }
        public string? Error { get; set; }
        public OllamaResponse? Response { get; set; }

        public static OllamaRestClientResponse Failure(string? error)
            => new()
                {
                    Success = false,
                    Error = error ?? "Unknown"
                };

        public static OllamaRestClientResponse Succeeded(OllamaResponse response)
            => new()
                {
                    Success = true,
                    Response = response
                };
    }
}