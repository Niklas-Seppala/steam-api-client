using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetUniqueUsers method
    /// </summary>
    public class GetUniqueUsers_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetUniqueUsers_Tests(ClientFixture fixture) : base(fixture) { }

        [Fact]
        public void DefaultParams_ReturnsUniqueUserCount()
        {
            var response = DotaApiClient.GetUniqueUsersAsync()
                .Result["users_last_month"];
            SleepAfterSendingRequest();

            Assert.True(response != 0);
        }
    }
}
