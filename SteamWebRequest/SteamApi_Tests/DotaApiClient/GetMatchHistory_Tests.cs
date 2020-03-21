using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using SteamApi;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client GetMatchHistory method.
    /// </summary>
    public class GetMatchHistory_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetMatchHistory_Tests(ClientFixture fixture) : base(fixture) { }


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
                return await DotaApiClient.GetMatchHistoryAsync(cToken: source.Token);
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
            var response = DotaApiClient.GetMatchHistoryAsync(apiInterface: "IDota_2_History")
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
            var response = DotaApiClient.GetMatchHistoryAsync(version: "v123")
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case where request match count
        /// is set to zero. Method throws ApiEmptyResponse
        /// Exception and fails. Method should return failed
        /// ApiResponse object.
        /// </summary>
        [Fact]
        public void CountSetToZero_RequestFails()
        {
            var response = DotaApiClient.GetMatchHistoryAsync(count: 0)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case where count is specified. Method should
        /// return only specified amount of matches.
        /// </summary>
        /// <param name="count">Requested count</param>
        /// <param name="resultCount">What result count should be</param>
        [Theory]
        [InlineData(50, 50)]
        [InlineData(100, 100)]
        [InlineData(200, 100)]
        [InlineData(500, 100)]
        [InlineData(5, 5)]
        [InlineData(1, 1)]
        public void CountParamDefined_ReturnsSpecifiedAmount(uint count, uint resultCount)
        {
            var response = DotaApiClient.GetMatchHistoryAsync(count: count)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.True(response.Contents.Count == resultCount);
        }


        /// <summary>
        /// Test case where account id is specified. Method should
        /// return only matches played by specified player.
        /// </summary>
        /// <param name="playerId">32-bit steam id</param>
        [Theory]
        [InlineData(78123870)]
        [InlineData(161273766)]
        [InlineData(386972442)]
        [InlineData(147169892)]
        public void AccountIdSpecified_ReturnsMatchesWithSpecifiedAccount(uint playerId)
        {
            var response = DotaApiClient.GetMatchHistoryAsync(id32: playerId, count: 5)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.All(response.Contents, match => {
                Assert.Contains(match.Players, (player) => player.Id32 == playerId);
            });
        }


        /// <summary>
        /// Test case where league id is specified. Method should
        /// return only matches from requested league.
        /// </summary>
        /// <param name="leagueId">League id</param>
        [Theory]
        [InlineData(5609)]  // ESL One Hamburg 2017
        [InlineData(11517)] // DreamLeague Season 13
        [InlineData(10749)] // The International 2019
        [InlineData(9870)]  // The International 2018
        public void LeagueIdSpecified_ReturnsMatchesFromSpecifiedLeague(uint leagueId)
        {
            var response = DotaApiClient.GetMatchHistoryAsync(leagueId: leagueId)
                .Result;
            SleepAfterSendingRequest();
            var detailsResponse = DotaApiClient.GetMatchDetailsAsync(
                response.Contents.ElementAt(0).Id).Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.Equal(leagueId, detailsResponse.Contents.LeagueId);
        }


        /// <summary>
        /// Test case where minimum human player count is specified.
        /// Method should return only matches with equal or more players
        /// than requested.
        /// </summary>
        /// <param name="minPlayerCount">minimum player count in match</param>
        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(20)]
        public void MinPlayersSpecified_ReturnsMatchesWithMinimumPLayerCount(uint minPlayerCount)
        {
            var response = DotaApiClient.GetMatchHistoryAsync(minPlayers: minPlayerCount)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.All(response.Contents, match => {
                Assert.True(match.Players.Count >= minPlayerCount);
            });
        }


        /// <summary>
        /// Test case for hero id specified. Method should return
        /// only matches with specified hero in them.
        /// </summary>
        /// <param name="heroId">Hero id</param>
        [Theory]
        [InlineData(80)]
        [InlineData(88)]
        [InlineData(36)]
        [InlineData(1)]
        [InlineData(66)]
        public void HeroSpecified_ReturnsOnlyGamesWithSpecifiedHero(uint heroId)
        {
            var response = DotaApiClient.GetMatchHistoryAsync(heroId: heroId, count: 2)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.All(response.Contents, match =>
            {
                var players = from p in match.Players
                              where p.HeroId == heroId
                              select p;
                Assert.Contains(players, (player) => player.HeroId == heroId);
            });
        }

        /// <summary>
        /// Test case for hero id specified. Method should return
        /// only matches with specified hero in them.
        /// </summary>
        /// <param name="heroId">Hero id</param>
        [Theory]
        [InlineData(DotaSkillLevel.Any)]
        [InlineData(DotaSkillLevel.Normal)]
        [InlineData(DotaSkillLevel.High)]
        [InlineData(DotaSkillLevel.VeryHigh)]
        public void SkillLevelSpecified_ReturnsOnlyGamesWithSpecifiedSkill(DotaSkillLevel skillLvl)
        {
            var response = DotaApiClient.GetMatchHistoryAsync(skillLevel: skillLvl)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.All(response.Contents, match =>
            {
                Assert.Equal(match.SkillLevel, (int)skillLvl);
            });
        }
    }
}
