using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetDotaTeam method
    /// </summary>
    public class GetDotaTeam_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetDotaTeam_Tests(ClientFixture fixture) : base(fixture) { }


        [Theory]
        [InlineData(36)]
        [InlineData(7668567)]
        public void TeamIdProvided_ReturnsTeam(uint teamId)
        {
            var response = DotaApiClient.GetDotaTeamAsync(teamId)
                .Result;
            SleepAfterSendingRequest();

            Assert.True(response.TeamId == teamId);
        }
    }
}
