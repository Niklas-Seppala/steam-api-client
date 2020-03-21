using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetItemInfos method
    /// </summary>
    public class GetItemInfos_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetItemInfos_Tests(ClientFixture fixture) : base(fixture) { }


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
        /// Test case for deafult parameters. Method should
        /// return Dictionary containing dota 2 item infos
        /// wrapped into ApiResponse object.
        /// </summary>
        /// <param name="itemName">name of the hero</param>
        [Fact]
        public void DefaultParams_ReturnsItemInfos()
        {
            var response = DotaApiClient.GetItemInfosAsync()
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
            Assert.All(response.Contents, item => {
                Assert.True(item.Value.Id != 0);
            });
        }

    }
}
