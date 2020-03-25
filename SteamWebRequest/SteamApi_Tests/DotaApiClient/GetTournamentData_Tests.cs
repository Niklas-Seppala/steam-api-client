using Xunit;

namespace Client.Dota
{
    public class GetTournamentData_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetTournamentData_Tests(ClientFixture fixture) : base(fixture) { }


        [Fact]
        public void GetTopLiveGames_DefaultParams_ReturnsTopLiveGames()
        {
            var topGames = DotaApiClient.GetTopLiveGamesAsync()
                .Result;
            SleepAfterSendingRequest();

            Assert.NotEmpty(topGames.Contents);
        }


        [Fact(Skip = "Event games are hard to come by.")]
        public void GetTopLiveEventGames_DefaultParams_ReturnsTopLiveEventGames()
        {
            var topEventGames = DotaApiClient.GetTopLiveEventGamesAsync()
                .Result;

            Assert.NotNull(topEventGames);
            Assert.NotEmpty(topEventGames);
        }
    }
}
