using Xunit;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetTournamentPlayerStats method.
    /// </summary>
    public class GetTournamentPlayerStats_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetTournamentPlayerStats_Tests(ClientFixture fixture) : base(fixture) { }


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
                return await DotaApiClient.GetTournamentPlayerStatsAsync(72312627,
                    9870, cToken: source.Token);
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
            var response = DotaApiClient.GetTournamentPlayerStatsAsync(72312627,
                9870, apiInterface: "IProDota").Result;
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
            var response = DotaApiClient.GetTournamentPlayerStatsAsync(72312627,
                9870, version: "v1.3").Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for valid player id and valid tournament id.
        /// Method should return player stats in that tournament
        /// wrapped into ApiResponse object.
        /// </summary>
        /// <param name="playerId">Pro dota 2 player 32-bit id.</param>
        /// <param name="eventId">Tournament id</param>
        [Theory]
        [InlineData(72312627, 9870)]  // TI8
        [InlineData(26771994, 10749)] // TI9
        [InlineData(26771994, 9870)]  // TI8
        public void ProPlayerIdProvided_ReturnsMatchesFromEventByPlayer
            (uint playerId, uint eventId)
        {
            var response = DotaApiClient.GetTournamentPlayerStatsAsync(playerId, eventId)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.Equal(playerId, response.Contents.Id32);
            Assert.NotEmpty(response.Contents.Matches);
            Assert.NotEmpty(response.Contents.HeroesPlayed);
        }


        /// <summary>
        /// Test case for specific hero included
        /// in the request. Method should return pro
        /// players stats of games played with specified
        /// hero. Wrapped into ApiResponse ofc.
        /// </summary>
        /// <param name="playerId">Pro player 32-bit id</param>
        /// <param name="eventId">Tournament id.</param>
        /// <param name="heroId">Dota 2 hero id.</param>
        [Theory]
        [InlineData(72312627, 9870, 42)]  // Wraith King
        [InlineData(26771994, 9870, 7)]   // Earthshaker
        [InlineData(19672354, 10749, 66)] // Chen
        public void HeroIdProvided_ReturnsMatchesWithSpecifiedHero(
            uint playerId, uint eventId, uint heroId)
        {
            var response = DotaApiClient.GetTournamentPlayerStatsAsync(playerId, eventId,
                 heroId: heroId).Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.Equal(playerId, response.Contents.Id32);
            Assert.NotEmpty(response.Contents.Matches);
            Assert.NotEmpty(response.Contents.HeroesPlayed);
            Assert.All(response.Contents.Matches, (match) => Assert.Equal(heroId, match.HeroId));
        }

        /// <summary>
        /// Test case for non pro player. Tournament id is valid.
        /// Method should return Failed ApiResponse object with
        /// null contents.
        /// </summary>
        [Fact]
        public void NonProId_ReturnsEmptyObject()
        {
            var response = DotaApiClient.GetTournamentPlayerStatsAsync(147169892, 9870)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case invalid tournament id. Player id is valid.
        /// Method should return Failed ApiResponse object with
        /// null contents.
        /// </summary>
        [Fact]
        public void InvalidTournamentId_ReturnsEmptyObject()
        {
            var response = DotaApiClient.GetTournamentPlayerStatsAsync(147169892, 0)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
            Assert.True(response.Status == 8);
            Assert.NotNull(response.StatusDetail);
        }
    }
}
