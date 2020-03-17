using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for GetMatchDetails method.
    /// </summary>
    public class GetMatchDetails_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetMatchDetails_Tests(ClientFixture fixture) : base(fixture) { }


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
            var task = Task.Run(async () => {
                return await DotaApiClient.GetMatchDetailsAsync(5215439388, cToken: source.Token);
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
            var response = DotaApiClient.GetMatchDetailsAsync(5215439388, apiInterface: "IDota_2_matches")
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
            var response = DotaApiClient.GetMatchHistoryBySequenceNumAsync(5215439388, version: "v393")
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for valid match id. Method should
        /// return match details model wrapped into ApiResponse
        /// object.
        /// </summary>
        /// <param name="matchId">Id of the dota 2 match</param>
        [Theory]
        [InlineData(5215439388)]
        [InlineData(5214286157)]
        [InlineData(5214240197)]
        [InlineData(5211180398)]
        [InlineData(5202107556)]
        [InlineData(5200756005)]
        public void ValidMatchIds_ReturnsCorrectMatches(ulong matchId)
        {
            var response = DotaApiClient.GetMatchDetailsAsync(matchId)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.Equal(matchId, response.Contents.MatchId);
        }
    }
}
