using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetHeroInfo method
    /// </summary>
    public class GetHeroInfo_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetHeroInfo_Tests(ClientFixture fixture) : base(fixture) { }

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
                return await DotaApiClient.GetHeroInfosAsync(cToken: source.Token);
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
        /// dictionary of heroinfos wrapped into ApiResponse
        /// object.
        /// </summary>
        /// <param name="heroName">Name of the hero</param>
        [Fact]
        public void DefaultArgs_ReturnsHeroInfo()
        {
            var response = DotaApiClient.GetHeroInfosAsync()
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.All(response.Contents, info =>
            {
                Assert.NotEmpty(info.Value.Name);
                Assert.NotNull(info.Value.Bio);
                Assert.NotEmpty(info.Value.AttackType);
                Assert.NotEmpty(info.Value.Roles);
            });
        }
    }
}
