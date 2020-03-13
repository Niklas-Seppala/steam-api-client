using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetRecentDcpEvents method
    /// </summary>
    public class GetRecentDcpEvents_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetRecentDcpEvents_Tests(ClientFixture fixture) : base(fixture) { }


        [Fact]
        public void DefaultParams_ReturnsRecentDcpEvents()
        {
            var response = DotaApiClient.GetRecentDcpEventsAsync()
                .Result;
            SleepAfterSendingRequest();

            Assert.NotNull(response);
            Assert.NotNull(response.Tournaments);
            Assert.True(response.WagerTimestamp != 0);
            Assert.NotEmpty(response.Tournaments);

            Assert.All(response.Tournaments, t => {
                Assert.True(t.Id != 0);
                Assert.NotEmpty(t.Matches);
                Assert.All(t.Matches, m =>
                {
                    Assert.True(m.NodeId != 0);
                });
                Assert.NotEmpty(t.Name);
            });

        }
    }
}
