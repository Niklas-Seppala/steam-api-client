using SteamApiClient;
using System;
using System.Diagnostics;
using Xunit;
using System.Net.Http;

namespace SWR.Client_Tests
{
    public class ConfigureClient_Tests
    {
        [Theory]
        [InlineData("invalid_dev_key")]
        [InlineData("")]
        public void DevKeyInvalid_ThrowsClientConfigException(string devKey)
        {
            Assert.Throws<HttpClientConfigException>(() =>
            {
                try
                {
                    SteamHttpClient client = new SteamHttpClient(devKey);
                }
                catch (Exception ex)
                {
                    Assert.IsType<HttpRequestException>(ex.InnerException);
                    throw;
                }
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
