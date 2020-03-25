using System.Threading;
using System.Threading.Tasks;
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


        /// <summary>
        /// Test case for request method being cancelled by CancellationToken.
        /// Method should return failed ApiResponse object that contains thrown
        /// cancellation exception.
        /// </summary>
        [Fact]
        public async Task MethodGotCancelled_RequestFails()
        {
            CancellationTokenSource source = new CancellationTokenSource();

            // Start task to be cancelled
            var task = Task.Run(async () =>
            {
                return await DotaApiClient.GetDotaTeamAsync(36, cToken: source.Token);
            });

            // Cancel method
            source.Cancel();

            var response = await task;
            SleepAfterSendingRequest();

            AssertRequestWasCancelled(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for invalid API method version being provided.
        /// Method should return failed ApiResponse object where exception
        /// that caused failure is stored.
        /// </summary>
        [Fact(Skip = "Arbitrary version doesn't matter??")]
        public void InvalidMethodVersion_RequestFails()
        {
            var response = DotaApiClient.GetDotaTeamAsync(36, version: "v1.2.3")
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for valid team id as parameter.
        /// Method should return requested team.
        /// </summary>
        /// <param name="teamId">Dota 2 team id</param>
        [Theory]
        [InlineData(36)]
        [InlineData(39)]
        [InlineData(2163)]
        [InlineData(46)]
        public void TeamIdProvided_ReturnsTeam(uint teamId)
        {
            var response = DotaApiClient.GetDotaTeamAsync(teamId)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents.Members);
            Assert.True(response.Contents.TeamId == teamId);
        }
    }
}
