using DotNetLlama.Enums;
using Mattncott.Extensions;

namespace DotNetLlama.Test.SerializationTests
{
    public class OllamaFormatTests
    {
        [Test]
        public void OllamaRequest_ToString_IsCorrect()
        {
            OllamaFormat.Json.GetStringValue().Should().Be("json");
        }
    }
}