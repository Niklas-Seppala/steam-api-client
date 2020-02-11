using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SteamApi;

namespace Client
{
    public class GetMatchDetails_Tests : SteamApiClientTests
    {
        public GetMatchDetails_Tests(ClientFixture fixture) : base(fixture){}

        [Theory]
        [InlineData("5215439388")]
        [InlineData("5214286157")]
        [InlineData("5214240197")]
        [InlineData("5211180398")]
        [InlineData("5202107556")]
        [InlineData("5200756005")]
        public void ValidMatchIds_ReturnsCorrectMatches(string matchId)
        {
            var details = Client.GetMatchDetailsAsync(matchId)
                .Result;
            SleepAfterApiCall();

            Assert.Equal(matchId, details.MatchId.ToString());
        }
    }
}
