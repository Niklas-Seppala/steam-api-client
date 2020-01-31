using SteamApiClient.Dota;
using Xunit;

namespace SWR.Client_Tests
{
    public class GetItemCreators_Tests : SteamHttpClient_Tests
    {
        [Theory]
        [InlineData(6666)]
        [InlineData(5613)]
        [InlineData(5614)]
        [InlineData(5615)]
        public void ItemDefsWithCreatorsProvided_ReturnsItemCreators(uint itemDef)
        {
            var creators = _client.GetItemCreatorsAsync(itemDef)
                .Result;
            this.Sleep();

            Assert.NotEmpty(creators);
        }

        [Theory]
        [InlineData(418)]
        [InlineData(419)]
        [InlineData(420)]
        [InlineData(421)]
        public void ItemDefsWithNoCreatorsProvided_ReturnsEmptyCreators(uint itemDef)
        {
            var creators = _client.GetItemCreatorsAsync(itemDef)
                .Result;
            this.Sleep();

            Assert.Empty(creators);
        }
    }
}
