using Microsoft.Extensions.DependencyInjection;
using DotNetLlama.Extensions;
using Microsoft.Extensions.Configuration;
using DotNetLlama.Interfaces;

namespace DotNetLlama.Test.Extensions
{
    public class ServiceCollectionExtensionsTests
    {
        private IConfiguration _configuration;

        [SetUp]
        public void Setup()
        {
            var inMemorySettings = new List<KeyValuePair<string, string?>> {
                new("Ollama:ApiToken", "apiToken"),
                new("Ollama:LlamaBaseUrl", "http://llamaBaseUrl"),
            };

            _configuration = CommonMocks.Configuration(inMemorySettings);
        }

        [Test]
        public void RegisterDotNetLlama_NoApiKey_ThrowsNullReferenceException()
        {
            var inMemorySettings = new List<KeyValuePair<string, string?>> {
                new("Ollama:LlamaBaseUrl", "http://llamaBaseUrl"),
            };

            var configuration = CommonMocks.Configuration(inMemorySettings);

            var serviceCollection = Substitute.For<IServiceCollection>();

            Action action = () => serviceCollection.RegisterDotNetLlama(configuration);
            action.Should().Throw<NullReferenceException>();
        }

        [Test]
        public void RegisterDotNetLlama_NoLlamaBaseUrl_ThrowsNullReferenceException()
        {
            var inMemorySettings = new List<KeyValuePair<string, string?>> {
                new("Ollama:ApiToken", "apiToken"),
            };

            var configuration = CommonMocks.Configuration(inMemorySettings);

            var serviceCollection = Substitute.For<IServiceCollection>();

            Action action = () => serviceCollection.RegisterDotNetLlama(configuration);
            action.Should().Throw<NullReferenceException>();
        }

        [Test]
        public void RegisterDotNetLlama_OptionsAreProvided_RegistersIDotNetLlamaOptions()
        {
            var serviceProvider = BuildServiceProvider();
            serviceProvider.GetRequiredService<IDotNetLlamaOptions>();
        }

        [TestCase(typeof(IOllamaRestClient))]
        public void RegisterDependencies_CorrectlyRegisters(Type requiredService)
        {
            var provider = BuildServiceProvider();
            provider.GetRequiredService(requiredService);
        }

        private ServiceProvider BuildServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            return serviceCollection.RegisterDotNetLlama(_configuration).BuildServiceProvider(true);
        }
    }
}