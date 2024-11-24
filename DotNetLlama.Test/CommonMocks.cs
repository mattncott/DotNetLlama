using Mattncott;
using Microsoft.Extensions.Configuration;

namespace DotNetLlama.Test
{
    public static class CommonMocks
    {
        public static IConfiguration Configuration(List<KeyValuePair<string, string?>> inMemorySettings, bool initialize = true)
        {
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            if (initialize)
            {
                ConfigurationHelper.Initialize(configuration);
            }

            return configuration;
        }
    }
}