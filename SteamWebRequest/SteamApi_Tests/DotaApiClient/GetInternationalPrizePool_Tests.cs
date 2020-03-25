using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's getInternationalPrizePool method.
    /// </summary>
    public class GetInternationalPrizePool_Tests : ApiTests
    {
        /// <summary>
        /// Setup.
        /// </summary>
        public GetInternationalPrizePool_Tests(ClientFixture fixture) : base(fixture) { }


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
                return await DotaApiClient.GetInternationalPrizePoolAsync(cToken: source.Token);
            });

            // Cancel method
            source.Cancel();

            var response = await task;
            SleepAfterSendingRequest();

            AssertRequestWasCancelled(response);
            Assert.True(response.Contents == 0);
        }


        /// <summary>
        /// Test case for default parameters. Method
        /// should return this years international tournament
        /// prize pool in dollars, wrapped into ApiResponse object.
        /// </summary>
        [Fact(Skip = "TI event not started.")]
        public void DefaultParams_ReturnsTI_Prizepool()
        {
            var response = DotaApiClient.GetInternationalPrizePoolAsync()
                .Result;

            AssertRequestWasSuccessful(response);
            Assert.True(response.Contents != 0);
        }
    }
}
