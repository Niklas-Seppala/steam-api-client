using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetReatimeMatchStats method.
    /// </summary>
    public class GetRealtimeMatchStats_Tests : ApiTests
    {
        /// <summary>
        /// Readonly list of live matches.
        /// </summary>
        private readonly IReadOnlyList<ulong> _liveMatchServerIds;


        /// <summary>
        /// Setup.
        /// </summary>
        public GetRealtimeMatchStats_Tests(ClientFixture fixture) : base(fixture)
        {
            var response = DotaApiClient.GetTopLiveGamesAsync()
                .Result;
            SleepAfterSendingRequest();
            if (response.Successful)
            {
                _liveMatchServerIds = response.Contents
                    .Where(m => m.ServerSteamId != 0)
                    .Select(m => m.ServerSteamId)
                    .Take(5)
                    .ToList();
                if (_liveMatchServerIds.Count == 0)
                {
                    throw new Exception("Can't get live matches from api");
                }
            }
            else
            {
                throw new Exception("Can't get live matches from api");
            }
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
                return await DotaApiClient.GetRealtimeMatchStatsAsync(_liveMatchServerIds[0],
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
        /// Test case for invalid API method version being provided.
        /// Method should return failed ApiResponse object where exception
        /// that caused failure is stored.
        /// </summary>
        [Fact]
        public void InvalidMethodVersion_RequestFails()
        {
            var response = DotaApiClient.GetRealtimeMatchStatsAsync(_liveMatchServerIds[0],
                version: "v1.2.3").Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case valid server ids. Method should
        /// return RealtimeMatchStats wrapped into ApiResponse
        /// object.
        /// </summary>
        [Fact]
        public void ValidServerIds_ReturnsRealtimeStats()
        {
            Assert.All(_liveMatchServerIds, serverId =>
            {
                var response = DotaApiClient.GetRealtimeMatchStatsAsync(serverId)
                    .Result;

                SleepAfterSendingRequest();

                AssertRequestWasSuccessful(response);
                Assert.NotNull(response.Contents);
                Assert.NotEmpty(response.Contents.GraphData.GoldGraph);
            });
        }


        /// <summary>
        /// Test case invalid server id. Method should
        /// return failed ApiResponse with null contents.
        /// </summary>
        [Fact]
        public void InvalidServerIds_ReturnsFailedApiResponse()
        {
            var response = DotaApiClient.GetRealtimeMatchStatsAsync(0)
                .Result;

            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }
    }
}
