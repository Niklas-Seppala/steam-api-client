using SteamApiClient;
using SteamApiClient.Dota;
using System;
using Xunit;

namespace SWR.Client_Tests
{
    public class GetPlayerItems_Tests : SteamHttpClient_Tests
    {
        [Theory]
        [InlineData(76561198107435620)]
        [InlineData(76561198038389598)]
        public void ValidPlayerId_ReturnsPlayersItems(ulong id)
        {
            var items = _client.GetPlayerItemsAsync(id)
                .Result;
            this.Sleep();

            Assert.NotEmpty(items.Items);
            foreach (var item in items.Items)
            {
                Assert.NotEqual((ulong)0, item.Id);
            }
        }

        [Fact]
        public void PrivateInventory_ReturnsWithStatus15()
        {
            var items = _client.GetPlayerItemsAsync(76561198059119066)
                .Result;
            this.Sleep();
            Assert.Equal(15, items.Status);
        }
    }
}
