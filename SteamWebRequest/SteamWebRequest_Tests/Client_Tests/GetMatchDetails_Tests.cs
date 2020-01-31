using SteamApiClient.Dota;
using Xunit;

namespace SWR.Client_Tests
{
    public class GetMatchDetails_Tests : SteamHttpClient_Tests
    {
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
            this.Sleep();

            Assert.Equal(matchId, details.MatchId.ToString());
        }
    }
}
