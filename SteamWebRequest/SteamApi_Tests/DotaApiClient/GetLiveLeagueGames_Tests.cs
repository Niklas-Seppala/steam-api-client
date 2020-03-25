using System.Threading;
using System.Threading.Tasks;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetLiveLeagueGames method.
    /// </summary>
    public class GetLiveLeagueGames_Tests : ApiTests
    {
        private readonly IReadOnlyList<(uint, ulong)> _matchIds;

        /// <summary>
        /// Setup
        /// </summary>
        public GetLiveLeagueGames_Tests(ClientFixture fixture) : base(fixture) 
        {
            var response = DotaApiClient.GetLiveLeagueGamesAsync()
                .Result;

            if (response.Successful)
            {
                _matchIds = response.Contents
                    .Where(m => m.LeagueId != 0 && m.MatchId != 0)
                    .Select(m => (m.LeagueId, m.MatchId))
                    .Take(5)
                    .ToList();
                if (_matchIds.Count == 0)
                    throw new Exception("couldn't get live league matches from api");
            }
            else
                throw new Exception("couldn't get live league matches from api");
        }


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
                return await DotaApiClient.GetLiveLeagueGamesAsync(cToken: source.Token);
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
            var response = DotaApiClient.GetHeroesAsync(version: "v1.3")
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for match id specified. Method
        /// should return List of LiveLeagueMatch wrapped into
        /// ApiResponse object.
        /// </summary>
        [Fact]
        public void LeagueIdSpecified_ReturnsLiveMatchStats()
        {
            uint[] leagueIds = _matchIds.Select(m => m.Item1).ToArray();
            Assert.All(leagueIds, id => {
                var response = DotaApiClient.GetLiveLeagueGamesAsync(leagueId32: id)
                    .Result;
                SleepAfterSendingRequest();

                AssertRequestWasSuccessful(response);
                Assert.NotNull(response.Contents);
            });
        }


        /// <summary>
        /// Test case for match id specified. Method
        /// should return List of LiveLeagueMatch wrapped into
        /// ApiResponse object.
        /// </summary>
        [Fact]
        public void MatchIdSpecified_ReturnsLiveMatchStats()
        {
            Assert.All(_matchIds, ids => {
                var response = DotaApiClient.GetLiveLeagueGamesAsync(matchId64: ids.Item2)
                    .Result;
                SleepAfterSendingRequest();

                AssertRequestWasSuccessful(response);
                Assert.NotNull(response.Contents);
            });
        }


        /// <summary>
        /// Test case for defalt parameters. Method should
        /// return list of LiveLeagueMatches wrapped into
        /// ApiResponse object.
        /// </summary>
        [Fact]
        public void DefaultParameters_ReturnsLiveLeagueGames()
        {
            var response = DotaApiClient.GetLiveLeagueGamesAsync()
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
        }
    }
}
