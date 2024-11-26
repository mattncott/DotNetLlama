using DotNetLlama.Interfaces;
using Newtonsoft.Json;

namespace DotNetLlama
{
    public class JsonSerializer : IJsonSerializer
    {
        private readonly JsonSerializerSettings _settings;

        public JsonSerializer()
        {
            _settings = new JsonSerializerSettings
            {
                Converters = JsonConverters.AllNewtonsoft()
            };
        }

        public T Deserialize<T>(string json)
            => JsonConvert.DeserializeObject<T>(json, _settings);

        public string Serialize<T>(T obj)
            => JsonConvert.SerializeObject(obj, _settings);
    }
}