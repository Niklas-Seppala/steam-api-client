using Xunit;
using SteamWebRequest;
using System.Net.Http;
using System.Reflection;
using System.Diagnostics;

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
            Assert.NotNull(SteamHttpClient.Client);
            Assert.Equal(SecretVariables.DevKey, client.DevKey);
        }
    }
}
