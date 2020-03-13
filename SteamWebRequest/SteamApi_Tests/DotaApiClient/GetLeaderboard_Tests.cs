using SteamApi;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetLeaderboard method
    /// </summary>
    public class GetLeaderboard_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetLeaderboard_Tests(ClientFixture fixture) : base(fixture) { }


        [Theory]
        [InlineData(DotaRegion.Europe)]
        [InlineData(DotaRegion.SEA)]
        [InlineData(DotaRegion.America)]
        [InlineData(DotaRegion.China)]
        public void ValidRegion_ReturnsCorrectRegionLeaderboard(DotaRegion region)
        {
            var response = DotaApiClient.GetLeaderboardAsync(region)
                .Result;
            SleepAfterSendingRequest();

            Assert.NotNull(response);
            Assert.NotEmpty(response.Players);
        }
    }
}
