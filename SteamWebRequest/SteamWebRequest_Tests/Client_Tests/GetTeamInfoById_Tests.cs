using SteamApiClient;
using SteamApiClient.Dota;
using System;
using Xunit;

namespace SWR.Client_Tests
{
    public class GetTeamInfoById_Tests : SteamHttpClient_Tests
    {

        [Theory]
        [InlineData(5, 5)]
        [InlineData(20, 20)]
        [InlineData(50, 50)]
        [InlineData(150, 100)]
        [InlineData(200, 100)]
        public void CountProvided_ReturnsCorrectAmount(byte count, byte resultCount)
        {
            var teams = _client.GetDotaTeamInfosByIdAsync(count: count)
                .Result;
            this.Sleep();

            Assert.Equal(resultCount, teams.Count);
        }
    }
}
