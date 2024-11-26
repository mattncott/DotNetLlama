namespace DotNetLlama.Test.SerializationTests
{
    public abstract class SerializationTestsBase
    {
        /**
         * @param objectToTest the object to serialize
         * @param expected omit to test the object against itself. Provide the string value to test against that
         */
        protected void TestSystemTextJson<T>(T objectToTest, string? expected = null)
        {
            var options = GetOptions();
            var json = System.Text.Json.JsonSerializer.Serialize(objectToTest, options);

            if (string.IsNullOrEmpty(expected))
            {
                var deserializedMessage = System.Text.Json.JsonSerializer.Deserialize<T>(json, options);
                deserializedMessage.Should().BeEquivalentTo(objectToTest);
            }
            else
            {
                json.Should().Be(expected);
            }
        }

        /**
         * @param objectToTest the object to serialize
         * @param expected omit to test the object against itself. Provide the string value to test against that
         */
        protected void TestNewtonsoftJson<T>(T objectToTest, string? expected = null)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(objectToTest, GetSettings());

            if (string.IsNullOrEmpty(expected))
            {
                var deserializedMessage = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json, GetSettings());
                deserializedMessage.Should().BeEquivalentTo(objectToTest);
            }
            else 
            {
                json.Should().Be(expected);
            }
        }

        private System.Text.Json.JsonSerializerOptions GetOptions()
        {
            var options = new System.Text.Json.JsonSerializerOptions();
            foreach (var converter in GetSystemTextJsonConverters())
            {
                options.Converters.Add(converter);
            }
            return options;
        }

        private Newtonsoft.Json.JsonSerializerSettings GetSettings()
        {
            var settings = new Newtonsoft.Json.JsonSerializerSettings();
            foreach (var converter in GetNewtonsoftConverters())
            {
                settings.Converters.Add(converter);
            }
            return settings;
        }

        protected abstract List<Newtonsoft.Json.JsonConverter> GetNewtonsoftConverters();

        protected abstract List<System.Text.Json.Serialization.JsonConverter> GetSystemTextJsonConverters();
    }
}