using Xunit;
using SteamApiClient.Dota;

namespace SWR.Client_Tests
{
    public class GetSchemaUrl_Tests : SteamHttpClient_Tests
    {
        [Fact]
        public void DefaultParams_ReturnsSchemaUrl()
        {
            string schema = _client.GetSchemaUrlAsync()
                .Result;
            this.Sleep();

            Assert.NotEmpty(schema);
        }
    }
}
