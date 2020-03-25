using System.Threading;
using System.Threading.Tasks;
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
                return await DotaApiClient.GetRecentDcpEventsAsync(cToken: source.Token);
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
            var response = DotaApiClient.GetRecentDcpEventsAsync(version: "v1.2.3")
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for default parameters. Method should
        /// return all recent DCP events wrapped into ApiResponse
        /// object.
        /// </summary>
        [Fact(Skip = "COVID-19: no DCP events :(")]
        public void DefaultParams_ReturnsRecentDcpEvents()
        {
            var response = DotaApiClient.GetRecentDcpEventsAsync()
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.True(response.Contents.WagerTimestamp != 0);
            Assert.NotEmpty(response.Contents.Tournaments);

            Assert.All(response.Contents.Tournaments, t => {
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
