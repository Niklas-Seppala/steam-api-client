using SteamApiClient;
using SteamApiClient.Dota;
using System;
using Xunit;

namespace SWR.Client_Tests
{
    public class GetTopLiveGames_Tests : SteamHttpClient_Tests
    {
        [Fact]
        public void DefaultParams_ReturnsTopLiveGames()
        {
            var topGames = _client.GetTopLiveGamesAsync()
                .Result;
            this.Sleep();

            Assert.NotEmpty(topGames);
        }
    }
}
