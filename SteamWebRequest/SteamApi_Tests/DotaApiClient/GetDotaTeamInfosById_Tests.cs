using Xunit;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetDotaTeamInfosById method.
    /// </summary>
    public class GetDotaTeamInfosById_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        /// <param name="fixture"></param>
        public GetDotaTeamInfosById_Tests(ClientFixture fixture) : base(fixture) { }


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
                return await DotaApiClient.GetDotaTeamInfosByIdAsync(cToken: source.Token);
            });

            // Cancel method
            source.Cancel();

            var response = await task;
            SleepAfterSendingRequest();

            AssertRequestWasCancelled(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for invalid API interface being provided.
        /// Method should return failed ApiResponse object where exception
        /// that caused failure is stored.
        /// </summary>
        [Fact]
        public void InvalidApiInterface_RequestFails()
        {
            var response = DotaApiClient.GetDotaTeamInfosByIdAsync(apiInterface: "IProDota")
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
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
            var response = DotaApiClient.GetDotaTeamInfosByIdAsync(version: "v1.3")
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        [Theory]
        [InlineData(5, 5)]
        [InlineData(20, 20)]
        [InlineData(50, 50)]
        [InlineData(150, 100)]
        [InlineData(200, 100)]
        public void GetDotaTeamInfosById_CountProvided_ReturnsCorrectAmount(
            uint count, int resultCount)
        {
            var response = DotaApiClient.GetDotaTeamInfosByIdAsync(count: count)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
            Assert.Equal(resultCount, response.Contents.Count);
        }
    }
}
