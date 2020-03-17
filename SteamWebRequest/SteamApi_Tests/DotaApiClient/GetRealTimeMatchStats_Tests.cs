using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for GetRealTimeMatchStats method.
    /// </summary>
    public class GetRealTimeMatchStats_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetRealTimeMatchStats_Tests(ClientFixture fixture) : base(fixture) { }

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
