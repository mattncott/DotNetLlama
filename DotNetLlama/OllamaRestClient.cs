using DotNetLlama.Enums;
using DotNetLlama.Interfaces;
using DotNetLlama.Models;
using IRestClient = Mattncott.Interfaces.IRestClient;
using Microsoft.Extensions.Logging;
using RestSharp;

namespace DotNetLlama
{
    public class OllamaRestClient : IOllamaRestClient
    {
        private readonly IDotNetLlamaOptions _options;
        private readonly IRestClient _restClient;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly ILogger _logger;

        public OllamaRestClient(
            IDotNetLlamaOptions options,
            IRestClient restClient,
            IJsonSerializer jsonSerializer,
            ILoggerFactory factory)
        {
            _options = options;
            _restClient = restClient;
            _jsonSerializer = jsonSerializer;
            _logger = factory.CreateLogger<OllamaRestClient>();
        }

        public async Task<OllamaRestClientResponse> GenerateCompletion(OllamaRequest ollamaRequest)
        {
            ollamaRequest.Format = OllamaFormat.Json;

            if (ollamaRequest.Stream)
            {
                _logger.LogTrace("Stream is not yet supported. Please change to use stream = false");
                throw new NotImplementedException("Stream is not yet supported");
            }

            try 
            {
                _restClient.SetUrl($"{_options.LlamaBaseUrl}/generate");
                var ollamaResponse = await SendOllamaRequest(ollamaRequest);

                if (!ollamaResponse.Done)
                {
                    _logger.LogTrace("OllamaResponse was marked as Not Done. Waiting {waitInterval} seconds and resending", _options.WaitInterval);
                    // TODO Will this be blocking on main thread??
                    Thread.Sleep(_options.WaitInterval);
                    return await GenerateCompletion(ollamaRequest);
                }

                return OllamaRestClientResponse.Succeeded(ollamaResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to GenerateCompletion");
                return OllamaRestClientResponse.Failure(null);
            }
        }

        private async Task<OllamaResponse> SendOllamaRequest(OllamaRequest ollamaRequest)
        {
            var restRequest = new RestRequest();
            restRequest.AddHeader("Authorization", $"Bearer {_options.ApiToken}");
            restRequest.AddHeader("Content-Type", "application/json");

            var serializedBody = _jsonSerializer.Serialize(ollamaRequest);

            restRequest.AddJsonBody(serializedBody);

            return await _restClient.PostAsync<OllamaResponse>(restRequest);
        }
    }
}