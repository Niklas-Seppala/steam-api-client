using SteamApiClient.Dota;
using System.Linq;
using Xunit;

namespace SWR.Client_Tests
{
    public class GetMatchHistoryBySequenceNum_Tests : SteamHttpClient_Tests
    {
        [Theory]
        [InlineData(666666, 666667)]
        [InlineData(808089, 808089)]
        [InlineData(400, 401)]
        [InlineData(100, 240)] // starts at 240 for some reason
        [InlineData(0, 240)]
        public void ValidSeqNum_ReturnsMatchesStartingFromSeqNum(ulong seqNum, ulong resultStartSeqNum)
        {
            var matches = GlobalSetup.Client.GetMatchHistoryBySequenceNumAsync(seqNum: seqNum, count: 1)
                .Result;
            this.Sleep();

            Assert.Equal(resultStartSeqNum, matches.ElementAt(0).MatchSequenceNum);
        }

        [Theory]
        [InlineData(5, 5)]
        [InlineData(25, 25)]
        [InlineData(200, 100)]
        [InlineData(60, 60)]
        [InlineData(1, 1)]
        public void CountDefined_ReturnsCorrectAmountOfGames(byte count, byte actualCount)
        {
            ulong seqNum = 55555050;

            var matches = GlobalSetup.Client.GetMatchHistoryBySequenceNumAsync(seqNum, count: count)
                .Result;
            this.Sleep();

            Assert.Equal(actualCount, matches.Count);
        }
    }
}
