using SteamApiClient;
using SteamApiClient.Dota;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xunit;

namespace SWR.Client_Tests
{
    public class GetMatchHistoryBySequenceNum_Tests
    {
        private readonly SteamHttpClient _client;
        // Not not bombard api with requests
        private readonly bool _sleepAfterTest;
        private int _sleepTimeMs = 500;

        public GetMatchHistoryBySequenceNum_Tests()
        {
            _client = new SteamHttpClient(SecretVariables.DevKey);
            _sleepAfterTest = true;
        }

        private void SleepAfterApiCall()
        {
            if (_sleepAfterTest)
            {
                Thread.Sleep(_sleepTimeMs);
            }
        }

        [Theory]
        [InlineData(666666, 666666)]
        [InlineData(50505000, 50505000)]
        [InlineData(400, 400)]
        [InlineData(0, 200)]
        public void ValidSeqNum_ReturnsMatchesStartingFromSeqNum(ulong seqNum, ulong resultStartSeqNum)
        {
            var matches = _client.GetMatchHistoryBySequenceNumAsync(seqNum: seqNum, count:1)
                .Result;
            SleepAfterApiCall();

            Assert.Equal(resultStartSeqNum, matches.ElementAt(0).MatchSequenceNum);
        }

        [Theory]
        [InlineData(5)]
        [InlineData(25)]
        [InlineData(200)]
        [InlineData(60)]
        [InlineData(1)]
        public void CountDefined_ReturnsCorrectAmountOfGames(byte count)
        {
            ulong seqNum = 55555050;

            var matches = _client.GetMatchHistoryBySequenceNumAsync(seqNum, count: count)
                .Result;
            SleepAfterApiCall();

            Assert.Equal(count, matches.Count);
        }
    }
}
