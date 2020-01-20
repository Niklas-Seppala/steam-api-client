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
                SteamHttpClient.ConfigureClient(devKey);
            });
        }

        [Fact]
        public void DevKeyValid_FieldsInitialized()
        {
            SteamHttpClient.ConfigureClient(SecretVariables.DevKey);
            HttpClient clientField = (HttpClient)typeof(SteamHttpClient)
                .GetField("_client", BindingFlags.Static | BindingFlags.NonPublic)
                .GetValue(null);

            string key = (string)typeof(SteamHttpClient)
                .GetField("_devKey", BindingFlags.Static | BindingFlags.NonPublic)
                .GetValue(null);

            Assert.NotNull(clientField);
            Assert.Equal(SecretVariables.DevKey, key);
        }
    }
}
