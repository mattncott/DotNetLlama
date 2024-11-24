namespace DotNetLlama.Test
{
    public class DotNetLlamaOptionsTests
    {
        [Test]
        public void DotNetLlamaOptions_UrlWithTrailingSlash_RemovesTrailingSlash()
        {
            var url = "http://localhost/";
            var apikey = "abc123";
            var options = new DotNetLlamaOptions(apikey, url);

            options.LlamaBaseUrl.Should().Be("http://localhost");
        }

        [Test]
        public void DotNetLlamaOptions_UrlWithoutTrailingSlash_DoesNotRemoveTrailingSlash()
        {
            var url = "http://localhost";
            var apikey = "abc123";
            var options = new DotNetLlamaOptions(apikey, url);

            options.LlamaBaseUrl.Should().Be("http://localhost");
        }
    }
}