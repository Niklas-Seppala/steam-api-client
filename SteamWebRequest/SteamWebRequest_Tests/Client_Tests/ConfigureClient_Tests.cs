using Xunit;
using SteamWebRequest;
using System.Net.Http;
using System.Reflection;

namespace SWR.Client_Tests
{
    public class ConfigureClient_Tests
    {
        [Theory]
        [InlineData("invalid_dev_key")]
        [InlineData("")]
        public void DevKeyInvalid_ThrowsClientConfigException(string devKey)
        {
            Assert.Throws<HttpClientConfigException>(() => {
                SteamHttpClient client = new SteamHttpClient(devKey);
            });
        }

        [Fact]
        public void DevKeyValid_FieldsInitialized()
        {
            var client = new SteamHttpClient(SecretVariables.DevKey);

            HttpClient clientField = (HttpClient)typeof(SteamHttpClient)
                .GetField("_client", BindingFlags.Static | BindingFlags.NonPublic)
                .GetValue(client);

            string key = (string)typeof(SteamHttpClient)
                .GetField("_devKey", BindingFlags.Static | BindingFlags.NonPublic)
                .GetValue(null);

            Assert.NotNull(clientField);
            Assert.Equal(SecretVariables.DevKey, client.DevKey);
        }
    }
}
