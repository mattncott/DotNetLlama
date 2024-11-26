using System.Text.Json;
using DotNetLlama.Interfaces;
using Mattncott.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetLlama.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string OllamaConfigOption = "Ollama:";

        private static IServiceCollection RegisterDotNetLlamaOptions(this IServiceCollection services, IConfiguration configuration)
        {
            var apiToken = configuration.GetValue<string>($"{OllamaConfigOption}ApiToken");
            var llamaBaseUrl = configuration.GetValue<string>($"{OllamaConfigOption}LlamaBaseUrl");

            if (string.IsNullOrEmpty(apiToken))
            {
                throw new NullReferenceException($"Failed to register DotNetLlama, {nameof(apiToken)} is not provided");
            }

            if (string.IsNullOrEmpty(llamaBaseUrl))
            {
                throw new NullReferenceException($"Failed to register DotNetLlama, {nameof(llamaBaseUrl)} is not provided");
            }

            services.AddSingleton<IDotNetLlamaOptions>(new DotNetLlamaOptions(apiToken, llamaBaseUrl));
            return services;
        }

        public static IServiceCollection RegisterDotNetLlama(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDotNetLlamaOptions(configuration)
                .AddLogging()
                .RegisterRestClient(
                    jsonSerializerSettings: GetJsonSerializerSettings());

            services.AddSingleton<IOllamaRestClient, OllamaRestClient>();
            services.AddSingleton<IJsonSerializer, JsonSerializer>();

            return services;
        }

        private static JsonSerializerOptions GetJsonSerializerSettings()
        {
            var serializerSettings = new JsonSerializerOptions();

            foreach (var converter in JsonConverters.AllSystemTextJson())
            {
                serializerSettings.Converters.Add(converter);
            }

            return serializerSettings;
        }
    }
}