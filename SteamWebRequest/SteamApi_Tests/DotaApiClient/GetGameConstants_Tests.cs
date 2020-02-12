using Xunit;

namespace Client
{
    public class GetGameConstants_Tests : SteamApiClientTests
    {
        public GetGameConstants_Tests(ClientFixture fixture) : base(fixture) { }

        [Fact]
        public void GetGameItems_DefaultParams_ReturnsGameItems()
        {
            var items = Client.GetGameItemsAsync()
                .Result;
            SleepAfterApiCall();

            Assert.All(items, item =>
            {
                Assert.NotEmpty(item.LocalizedName);
                Assert.NotEmpty(item.Name);
                Assert.NotEqual(0, item.Id);
            });
        }

        [Fact]
        public void GetHeroes_DefaultParams_ReturnsHeroes()
        {
            var heroes = Client.GetHeroesAsync()
                .Result;
            SleepAfterApiCall();

            Assert.NotEmpty(heroes);
            Assert.All(heroes, hero =>
            {
                Assert.NotEmpty(hero.LocalizedName);
                Assert.NotEmpty(hero.Name);
                Assert.NotEqual(0, hero.Id);
            });
        }
    }
}
