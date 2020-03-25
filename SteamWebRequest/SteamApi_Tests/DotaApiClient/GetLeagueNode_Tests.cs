using Xunit;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetLeagueNode method
    /// </summary>
    public class GetLeagueNode_Tests : ApiTests
    {
        private readonly IReadOnlyList<(uint, uint)> _leagueIds;

        /// <summary>
        /// Setup
        /// </summary>
        public GetLeagueNode_Tests(ClientFixture fixture) : base(fixture) 
        {
            var response = DotaApiClient.GetLiveLeagueGamesAsync()
                .Result;
            if (response.Successful)
            {
                _leagueIds = response.Contents
                    .Where(m => m.LeagueId != 0 && m.LeagueNodeId != 0)
                    .Select(m => (m.LeagueId, m.LeagueNodeId))
                    .Take(5)
                    .ToList();
                if (_leagueIds.Count == 0)
                    throw new Exception("Couldn't get leagues from api");
            }
            else
                throw new Exception("Couldn't get leagues from api");
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
                return await DotaApiClient.GetLeagueNodeAsync(0, 0, cToken: source.Token);
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
            var response = DotaApiClient.GetLeagueNodeAsync(0, 0, version: "v1.2.3")
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for valid league and node
        /// ids as parameters. Method should return
        /// correct tournament node wrapped into ApiResponse
        /// object.
        /// </summary>
        [Fact]
        public void ValidTournamentIdAndNodeId_ReturnNode()
        {
            Assert.All(_leagueIds, id =>
            {
                var response = DotaApiClient.GetLeagueNodeAsync(id.Item1, id.Item2) // league and node id
                    .Result;
                SleepAfterSendingRequest();

                AssertRequestWasSuccessful(response);
                Assert.NotNull(response.Contents);
                Assert.Equal(response.Contents.NodeId, id.Item2);
            });
        }
    }
}
