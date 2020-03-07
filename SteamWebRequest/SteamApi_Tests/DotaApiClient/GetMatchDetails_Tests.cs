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
            var details = DotaApiClient.GetMatchDetailsAsync(matchId)
                .Result;
            SleepAfterSendingRequest();

            Assert.Equal(matchId, details.MatchId.ToString());
        }

        [Fact]
        public void GetRealTimeMatchStats_LiveGamesAvailable_RealTimeMatchStats()
        {
            var result = DotaApiClient.GetTopLiveGamesAsync().Result;
            SleepAfterSendingRequest();
            foreach (var liveGame in result)
            {
                var matchStats = DotaApiClient.GetRealtimeMatchStatsAsync(liveGame.ServerSteamId.ToString())
                    .Result;
                SleepAfterSendingRequest();

                Assert.NotNull(matchStats);
            }
        }
    }
}
