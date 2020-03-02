using Xunit;

namespace Client
{
    public class GetMatchDetails_Tests : SteamApiClientTests
    {
        public GetMatchDetails_Tests(ClientFixture fixture) : base(fixture) { }

        [Theory]
        [InlineData("5215439388")]
        [InlineData("5214286157")]
        [InlineData("5214240197")]
        [InlineData("5211180398")]
        [InlineData("5202107556")]
        [InlineData("5200756005")]
        public void ValidMatchIds_ReturnsCorrectMatches(string matchId)
        {
            var details = Client.GetMatchDetailsAsync(matchId)
                .Result;
            SleepAfterApiCall();

            Assert.Equal(matchId, details.MatchId.ToString());
        }

        [Fact]
        public void GetRealTimeMatchStats_LiveGamesAvailable_RealTimeMatchStats()
        {
            var result = Client.GetTopLiveGamesAsync().Result;
            SleepAfterApiCall();
            foreach (var liveGame in result)
            {
                var matchStats = Client.GetRealtimeMatchStatsAsync(liveGame.ServerSteamId.ToString())
                    .Result;
                SleepAfterApiCall();

                Assert.NotNull(matchStats);
            }
        }
    }
}
