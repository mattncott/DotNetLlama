using DotNetLlama.Interfaces;
using DotNetLlama.Models;
using Mattncott.Interfaces;
using Microsoft.Extensions.Logging;
using NSubstitute.ExceptionExtensions;

namespace DotNetLlama.Test
{
    public class OllamaRestClientTests
    {
        private IRestClient _restClient;
        private IDotNetLlamaOptions _options;
        private OllamaRestClient _ollamaRestClient;

        [SetUp]
        public void Setup()
        {
            _restClient = Substitute.For<IRestClient>();
            _options = Substitute.For<IDotNetLlamaOptions>();

            _ollamaRestClient = new OllamaRestClient(
                _options,
                _restClient,
                Substitute.For<IJsonSerializer>(),
                Substitute.For<ILoggerFactory>());
        }

        [Test]
        public void GenerateCompletion_OllamaRequestHasStream_IsNotSupported()
        {
            var request = new OllamaRequest
            {
                Model = "TestModel",
                Prompt = "Why is the sky blue",
                Stream = true
            };

            Func<Task> func = async () => await _ollamaRestClient.GenerateCompletion(request);
            func.Should().ThrowAsync<NotSupportedException>();
        }

        [Test]
        public async Task GenerateCompletion_ExceptionThrownWhenQueryingOllama_ReturnsFailedResponse()
        {
            var request = new OllamaRequest
            {
                Model = "TestModel",
                Prompt = "Why is the sky blue",
            };

            _restClient.PostAsync<OllamaResponse>(Arg.Any<RestSharp.RestRequest>()).Throws<TimeoutException>();

            var response = await _ollamaRestClient.GenerateCompletion(request);
            response.Success.Should().BeFalse();
            response.Error.Should().Be("Unknown");
        }

        [Test]
        public async Task GenerateCompletion_CorrectlyParsesResponse()
        {
            var request = new OllamaRequest
            {
                Model = "TestModel",
                Prompt = "Why is the sky blue",
            };

            var ollamaResponse = new OllamaResponse()
            {
                Model = "TestModel",
                CreatedAt = DateTime.Now,
                Response = "Because it is",
                Done = true
            };
            _restClient.PostAsync<OllamaResponse>(Arg.Any<RestSharp.RestRequest>()).Returns(ollamaResponse);

            var response = await _ollamaRestClient.GenerateCompletion(request);
            response.Success.Should().BeTrue();
            response.Error.Should().BeNull();
            response.Response.Should().Be(ollamaResponse);
        }

        [Test]
        public async Task GenerateCompletion_FirstResponseIsntDone_ResendToGetDoneResponse()
        {
            var request = new OllamaRequest
            {
                Model = "TestModel",
                Prompt = "Why is the sky blue",
            };

            var firstResponse = new OllamaResponse()
            {
                Model = "TestModel",
                CreatedAt = DateTime.Now,
                Response = "",
                Done = false
            };
            var secondResponse = new OllamaResponse()
            {
                Model = "TestModel",
                CreatedAt = DateTime.Now,
                Response = "Because it is",
                Done = true
            };
            _restClient.PostAsync<OllamaResponse>(Arg.Any<RestSharp.RestRequest>())
                .Returns(firstResponse, secondResponse);

            var response = await _ollamaRestClient.GenerateCompletion(request);
            response.Success.Should().BeTrue();
            response.Error.Should().BeNull();
            response.Response.Should().Be(secondResponse);

            _ = _restClient.Received(2).PostAsync<OllamaResponse>(Arg.Any<RestSharp.RestRequest>());
        }
    }
}