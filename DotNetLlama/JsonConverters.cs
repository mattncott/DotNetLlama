using DotNetLlama.Enums;

namespace DotNetLlama
{
    public static class JsonConverters
    {
        public static IList<Newtonsoft.Json.JsonConverter> AllNewtonsoft()
            => [
                new Mattncott.JsonConverters.Newtonsoft.EnumConverter<OllamaFormat>()
            ];

        public static IList<System.Text.Json.Serialization.JsonConverter> AllSystemTextJson()
            => [
                new Mattncott.JsonConverters.SystemTextJson.EnumConverter<OllamaFormat>()
            ];
    }
}