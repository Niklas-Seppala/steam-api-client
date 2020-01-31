using SteamApiClient.Dota;
using Xunit;

namespace SWR.Client_Tests
{
    public class GetLiveLeagueGames_Tests : SteamHttpClient_Tests
    {
        [Fact]
        public void DefaultParameters_ReturnsLiveLeagueGames()
        {
            var leagueGames = _client.GetLiveLeagueMatchAsync()
                .Result;

            Assert.NotEmpty(leagueGames);
        }
    }
}
