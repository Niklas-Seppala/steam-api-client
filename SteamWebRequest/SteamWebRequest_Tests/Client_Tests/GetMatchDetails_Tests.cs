using SteamApiClient;
using SteamApiClient.Dota;
using SWR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Xunit;

namespace SWR.Client_Tests
{
    public class GetMatchDetails_Tests
    {
        private readonly SteamHttpClient _client;
        // Not not bombard api with requests
        private readonly bool _sleepAfterTest;
        private int _sleepTimeMs = 500;

        public GetMatchDetails_Tests()
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
        [InlineData("5215439388")]
        [InlineData("5214286157")]
        [InlineData("5214240197")]
        [InlineData("5211180398")]
        [InlineData("5202107556")]
        [InlineData("5200756005")]
        public void ValidMatchIds_ReturnsCorrectMatches(string matchId)
        {
            var details = _client.GetMatchDetailsAsync(matchId)
                .Result;

            Assert.Equal(matchId, details.MatchId.ToString());

            SleepAfterApiCall();
        }
    }
}
