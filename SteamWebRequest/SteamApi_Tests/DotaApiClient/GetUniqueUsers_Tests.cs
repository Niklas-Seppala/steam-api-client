using System.Threading;
using System.Threading.Tasks;
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
                return await DotaApiClient.GetTopLiveGamesAsync(cToken: source.Token);
            });

            // Cancel method
            source.Cancel();

            var response = await task;
            SleepAfterSendingRequest();

            AssertRequestWasCancelled(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for default parameters. Method should
        /// return Dictionary containig unique dota 2 player count
        /// wrapped into ApiResponse object.
        /// </summary>
        [Fact]
        public void DefaultParams_ReturnsUniqueUserCount()
        {
            var response = DotaApiClient.GetUniqueUsersAsync()
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
        }
    }
}
