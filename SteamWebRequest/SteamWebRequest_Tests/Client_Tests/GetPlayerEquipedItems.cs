using Xunit;
using SteamApiClient.Dota;

namespace SWR.Client_Tests
{
    public class GetPlayerEquipedItems : SteamHttpClient_Tests
    {

        [Theory]
        [InlineData(76561198107435620, 1)]
        [InlineData(76561198038389598, 20)]
        [InlineData(76561198038389598, 8)]
        [InlineData(76561198038389598, 88)]
        public void ValidPlayerId_ReturnsPlayersEquipedItems(ulong id, byte heroId)
        {
            var items = _client.GetEquipedPlayerItemsAsync(id, heroId)
                .Result;
            this.Sleep();

            Assert.NotEmpty(items);
        }

        [Fact]
        public void PrivateAccount_Returns()
        {
            var items = _client.GetEquipedPlayerItemsAsync(76561198059119066, 1)
                .Result;
            this.Sleep();

            Assert.Empty(items);
        }
    }

}
