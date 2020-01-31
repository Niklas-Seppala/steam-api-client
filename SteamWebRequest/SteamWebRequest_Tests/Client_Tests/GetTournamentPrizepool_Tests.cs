using SteamApiClient.Dota;
using Xunit;

namespace SWR.Client_Tests
{
    public class GetTournamentPrizepool_Tests : SteamHttpClient_Tests
    {
        
        [Theory]
        [InlineData(9870, 25532177)]
        [InlineData(11517, 1000000)]
        public void LeagueIdProvided_ReturnsCorrectPrizepool(uint leagueId, uint prize)
        {
            var resp = _client.GetTournamentPrizePoolAsync(leagueId)
                .Result;
            this.Sleep();

            Assert.Equal(prize, resp["prize_pool"]);
            Assert.Equal(leagueId, resp["league_id"]);
        }
    }
}
