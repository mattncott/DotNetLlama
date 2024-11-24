using System.Security;
using DotNetLlama.Interfaces;

namespace DotNetLlama
{
    public class DotNetLlamaOptions: IDotNetLlamaOptions
    {
        public string ApiToken { get; set; }
        public string LlamaBaseUrl { get; set; }
        /**
         * WaitInterval is the amount of time to wait
         * before checking that a request is marked as Done
         */
        public int WaitInterval { get; set; } = 5;

        public DotNetLlamaOptions(string apiToken, string llamaBaseUrl)
        {
            this.ApiToken = apiToken;
            this.LlamaBaseUrl = TrimIfLastSlash(llamaBaseUrl);
        }

        private static string TrimIfLastSlash(string url)
        {
            if (url.EndsWith('/'))
            {
                return url.Remove(url.Length -1);
            }

            return url;
        }
    }
}
