using DotNetLlama.Enums;
using DotNetLlama.Models;

namespace DotNetLlama.Test.SerializationTests
{
    public class OllamaRequestTests: SerializationTestsBase
    {
        [Test]
        public void OllamaRequestCanBeSerialized_WhenUsingNewtonsoft()
        {
            var ollamaRequest = new OllamaRequest()
            {
                Model = "llama3.2",
                Prompt = "Why is the sky blue?",
                Format = OllamaFormat.Json
            };

            TestNewtonsoftJson(ollamaRequest);
            TestNewtonsoftJson(ollamaRequest, "{\"model\":\"llama3.2\",\"prompt\":\"Why is the sky blue?\",\"stream\":false,\"format\":\"json\"}");
        }

        [Test]
        public void OllamaRequestCanBeSerialized_WhenUsingSystemTextJson()
        {
            var ollamaRequest = new OllamaRequest()
            {
                Model = "llama3.2",
                Prompt = "Why is the sky blue?",
                Format = OllamaFormat.Json
            };

            TestSystemTextJson(ollamaRequest);
            TestSystemTextJson(ollamaRequest, "{\"model\":\"llama3.2\",\"prompt\":\"Why is the sky blue?\",\"stream\":false,\"format\":\"json\"}");
        }

        protected override List<Newtonsoft.Json.JsonConverter> GetNewtonsoftConverters()
            => [
                new Mattncott.JsonConverters.Newtonsoft.EnumConverter<OllamaFormat>()
            ];

        protected override List<System.Text.Json.Serialization.JsonConverter> GetSystemTextJsonConverters()
            => [
                new Mattncott.JsonConverters.SystemTextJson.EnumConverter<OllamaFormat>()
            ];
    }
}