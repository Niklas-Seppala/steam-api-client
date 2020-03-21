using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota api client's GetAbilities method
    /// </summary>
    public class GetAbilities_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetAbilities_Tests(ClientFixture fixture) : base(fixture) { }


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
        /// return Dictionary of dota 2 abilities wrapped into
        /// ApiResponse object.
        /// </summary>
        [Fact]
        public void DefaultParams_ReturnsAbilitites()
        {
            var response = DotaApiClient.GetAbilitiesAsync()
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
            Assert.All(response.Contents, ability =>
            {
                Assert.NotEmpty(ability.Value.Affects);
                Assert.NotNull(ability.Value.Attribute);
                Assert.NotEmpty(ability.Value.LocalizedName);
            });
        }
    }
}
