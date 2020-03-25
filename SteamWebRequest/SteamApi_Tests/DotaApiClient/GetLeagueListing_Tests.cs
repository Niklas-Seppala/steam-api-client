using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetLeagueListing method.
    /// </summary>
    public class GetLeagueListing_Tests : ApiTests
    {
        /// <summary>
        /// Setup.
        /// </summary>
        public GetLeagueListing_Tests(ClientFixture fixture) : base(fixture) { }


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
                return await DotaApiClient.GetLeagueListingAsync(cToken: source.Token);
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
        [Fact]
        public void InvalidMethodVersion_RequestFails()
        {
            var response = DotaApiClient.GetLeagueListingAsync(version: "v1.2.3")
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for default parameters. Method
        /// should return list of leagues wrapped into
        /// ApiResponse object.
        /// </summary>
        [Fact]
        public void DefaultParams_ReturnsLeagueListing()
        {
            var response = DotaApiClient.GetLeagueListingAsync()
                .Result;

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
        }
    }
}
