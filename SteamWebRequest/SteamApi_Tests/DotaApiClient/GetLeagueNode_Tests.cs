using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetLeagueNode method
    /// </summary>
    public class GetLeagueNode_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetLeagueNode_Tests(ClientFixture fixture) : base(fixture) { }

        /// <summary>
        /// Uses recent event nodes
        /// </summary>
        [Fact]
        public void ValidTournamentIdAndNodeId_ReturnNode()
        {
            var recentEvents = DotaApiClient.GetRecentDcpEventsAsync()
                .Result;
            SleepAfterSendingRequest();

            foreach (var tournament in recentEvents.Tournaments)
            {
                foreach (var match in tournament.Matches)
                {
                    var node = DotaApiClient.GetLeagueNodeAsync(tournament.Id, match.NodeId)
                        .Result;
                    Assert.NotNull(node);
                    Assert.True(node.NodeId == match.NodeId);
                }
            }
        }
    }
}
