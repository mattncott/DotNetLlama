using DotNetLlama.Models;

namespace DotNetLlama.Interfaces
{
    public interface IOllamaRestClient
    {
        // TODO <T> for template to return the response as
        // SEE https://github.com/ollama/ollama/blob/main/docs/api.md#generate-a-completion
        Task<OllamaRestClientResponse> GenerateCompletion(OllamaRequest ollamaRequest);
    }
}