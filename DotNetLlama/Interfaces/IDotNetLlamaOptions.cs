namespace DotNetLlama.Interfaces
{
    public interface IDotNetLlamaOptions
    {
        string ApiToken { get; set; }
        string LlamaBaseUrl { get; set; }
        int WaitInterval { get; set; }
    }
}