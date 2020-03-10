using Xunit;

namespace Client
{
    public class GetGameConstants_Tests : SteamApiClientTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetGameConstants_Tests(ClientFixture fixture) : base(fixture) { }


        [Fact]
        public void GetGameItems_DefaultParams_ReturnsGameItems()
        {
            var items = DotaApiClient.GetGameItemsAsync()
                .Result;
            SleepAfterSendingRequest();

            Assert.All(items, item =>
            {
                Assert.NotEmpty(item.LocalizedName);
                Assert.NotEmpty(item.Name);
                Assert.NotEqual((uint)0, item.Id);
            });
        }


        [Fact]
        public void GetHeroes_DefaultParams_ReturnsHeroes()
        {
            var heroes = DotaApiClient.GetHeroesAsync()
                .Result;
            SleepAfterSendingRequest();

            Assert.NotEmpty(heroes);
            Assert.All(heroes, hero =>
            {
                Assert.NotEmpty(hero.LocalizedName);
                Assert.NotEmpty(hero.Name);
                Assert.NotEqual((uint)0, hero.Id);
            });
        }
    }
}
