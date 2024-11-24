using System.Security;
using DotNetLlama.Interfaces;

namespace DotNetLlama
{
    public class DotNetLlamaOptions: IDotNetLlamaOptions
    {
        public string ApiToken { protected get; set; }
        public string LlamaBaseUrl { protected get; set; }

        public DotNetLlamaOptions(string apiToken, string llamaBaseUrl)
        {
            this.ApiToken = apiToken;
            this.LlamaBaseUrl = llamaBaseUrl;
        }
    }
}
