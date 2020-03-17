using System.Linq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetMatchHistoryBySequenceNum method
    /// </summary>
    public class GetMatchHistoryBySequenceNum_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetMatchHistoryBySequenceNum_Tests(ClientFixture fixture) : base(fixture) { }


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
                return await DotaApiClient.GetMatchHistoryBySequenceNumAsync(808089, count: 1, cToken: source.Token);
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
            var response = DotaApiClient.GetMatchHistoryBySequenceNumAsync(808089, count: 1, apiInterface: "IDota_2_matches")
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
            var response = DotaApiClient.GetMatchHistoryBySequenceNumAsync(808089, count: 1, version: "v33")
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for valid sequence num being provided.
        /// Method should return match with matching sequence number.
        /// </summary>
        /// <param name="seqNum">match sequence number</param>
        /// <param name="resultSeqNum">what result sequence number should be</param>
        [Theory]
        [InlineData(666666, 666667)]
        [InlineData(808089, 808089)]
        [InlineData(400, 401)]
        [InlineData(100, 240)] // starts at 240 for some reason
        [InlineData(0, 240)]
        public void ValidSeqNum_ReturnsMatchesStartingFromSeqNum(
            ulong seqNum, ulong resultSeqNum)
        {
            var response = DotaApiClient.GetMatchHistoryBySequenceNumAsync(seqNum, count: 1)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.Equal(resultSeqNum, response.Contents.ElementAt(0).MatchSequenceNum);
        }


        /// <summary>
        /// Test case for count specified. Method
        /// should return requested amount of matches.
        /// </summary>
        /// <param name="count">call size</param>
        /// <param name="resultCount">what result count should be</param>
        [Theory]
        [InlineData(5, 5)]
        [InlineData(25, 25)]
        [InlineData(200, 100)]
        [InlineData(60, 60)]
        [InlineData(1, 1)]
        public void CountDefined_ReturnsCorrectAmountOfGames(uint count, byte resultCount)
        {
            ulong seqNum = 55555050;
            var response = DotaApiClient.GetMatchHistoryBySequenceNumAsync(seqNum, count: count)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.Equal(resultCount, response.Contents.Count);
        }
    }
}
