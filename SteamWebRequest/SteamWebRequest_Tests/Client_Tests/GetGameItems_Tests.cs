using SteamApiClient.Dota;
using Xunit;

namespace SWR.Client_Tests
{
    public class GetGameItems_Tests : SteamHttpClient_Tests
    {
        [Fact]
        public void DefaultParams_ReturnsGameItems()
        {
            var items = _client.GetGameItemsAsync()
                .Result;
            this.Sleep();

            Assert.All(items, item => {
                Assert.NotEmpty(item.Name);
                Assert.NotEqual(0, item.Id);
            });
        }
    }
}
