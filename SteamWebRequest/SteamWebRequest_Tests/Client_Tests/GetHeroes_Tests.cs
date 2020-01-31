using SteamApiClient.Dota;
using Xunit;

namespace SWR.Client_Tests
{
    public class GetHeroes_Tests : SteamHttpClient_Tests
    {
        [Fact]
        public void DefaultParams_ReturnsHeroes()
        {
            var heroes = _client.GetHeroesAsync()
                .Result;

            Assert.NotEmpty(heroes);
            Assert.All(heroes, hero => {
                Assert.NotEmpty(hero.Name);
                Assert.NotEqual(0, hero.Id);
            });
        }
    }
}
