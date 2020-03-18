using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetHeroStats method
    /// </summary>
    public class GetHeroStats_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetHeroStats_Tests(ClientFixture fixture) : base(fixture) { }


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
        /// Tests that method returns filled
        /// dictionary of herostats.
        /// </summary>
        /// <param name="heroName">name of the hero</param>
        [Fact]
        public void DefaultParams_ReturnsHeroStats()
        {
            var response = DotaApiClient.GetHeroStatsAsync()
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.All(response.Contents, stats =>
            {
                Assert.NotEmpty(stats.Value.Name);
                Assert.NotNull(stats.Value.Attributes);
                Assert.NotEmpty(stats.Value.LocalizedName);
                Assert.NotEmpty(stats.Value.PrimaryAttribute);
            });
        }
    }
}
