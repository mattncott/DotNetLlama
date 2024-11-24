namespace DotNetLlama.Interfaces
{
    public interface IDotNetLlamaOptions
    {
        string ApiToken { set; }
        string LlamaBaseUrl { set; }
    }
}