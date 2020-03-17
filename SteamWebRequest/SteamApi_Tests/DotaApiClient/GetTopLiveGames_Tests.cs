using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for GetTopLiveGames method.
    /// </summary>
    public class GetTopLiveGames_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetTopLiveGames_Tests(ClientFixture fixture) : base(fixture) { }

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
        /// Test case for invalid API interface being provided.
        /// Method should return failed ApiResponse object where exception
        /// that caused failure is stored.
        /// </summary>
        [Fact]
        public void InvalidApiInterface_RequestFails()
        {
            var response = DotaApiClient.GetTopLiveGamesAsync(apiInterface: "IDota_2_matches")
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
            var response = DotaApiClient.GetTopLiveGamesAsync(version: "v393")
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case where valid partner parameter is provided.
        /// Method should return top live games wrapped into
        /// ApiResponse object.
        /// </summary>
        /// <param name="partner">steam partner</param>
        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        public void ValidPartnerDefined_ReturnsTopLiveGames(uint partner)
        {
            var response = DotaApiClient.GetTopLiveGamesAsync(partner: partner)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
        }


        /// <summary>
        /// Test case where invalid partner parameter is provided.
        /// Method should return failed api response object.
        /// </summary>
        /// <param name="partner">steam partner</param>
        [Theory]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        public void InvalidPartnerDefined_RequestFails(uint partner)
        {
            var response = DotaApiClient.GetTopLiveGamesAsync(partner: partner)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for default parameters. Method
        /// should return current top live matches wrapped
        /// into ApiResponse object.
        /// </summary>
        [Fact]
        public void GetRealTimeMatchStats_LiveGamesAvailable_RealTimeMatchStats()
        {
            var response = DotaApiClient.GetTopLiveGamesAsync()
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
        }
    }
}
