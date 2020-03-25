using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetTournamentPrizePool method.
    /// </summary>
    public class GetTournamentPrizePool_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetTournamentPrizePool_Tests(ClientFixture fixture) : base(fixture) { }


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
                return await DotaApiClient.GetTournamentPrizePoolAsync(11517,
                    cToken: source.Token);
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
            var response = DotaApiClient.GetTournamentPrizePoolAsync(11517,
                apiInterface: "IProDota").Result;
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
            var response = DotaApiClient.GetTournamentPrizePoolAsync(11517,
                version: "v2.3").Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for league id specified. Method
        /// should return TournamentPrizePool object wrapped into
        /// ApiResponse object.
        /// </summary>
        /// <param name="leagueId">Tournament id</param>
        /// <param name="correctPrizePool">What prize pool should be</param>
        [Theory]
        [InlineData(9870, 25532177)]
        [InlineData(11517, 1000000)]
        [InlineData(10749, 34330068)]
        public void GetTournamentPrizePool_LeagueIdProvided_ReturnsCorrectPrizepool(
            uint leagueId, uint correctPrizePool)
        {
            var response = DotaApiClient.GetTournamentPrizePoolAsync(leagueId)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.Equal(response.Contents.LeagueId, leagueId);
            Assert.Equal(response.Contents.PrizePool, correctPrizePool);
        }
    }
}
