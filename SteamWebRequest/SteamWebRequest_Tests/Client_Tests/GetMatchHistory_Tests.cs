using SteamApiClient.Dota;
using System.Linq;
using Xunit;

namespace SWR.Client_Tests
{
    public class GetMatchHistory_Tests : SteamHttpClient_Tests
    {
        [Theory]
        [InlineData(50, 50)]
        [InlineData(100, 100)]
        [InlineData(200, 100)]
        [InlineData(5, 5)]
        [InlineData(1, 1)]
        [InlineData(0, 0)]
        public void CountParamDefined_ReturnsSpecifiedAmount(byte count, byte resultCount)
        {
            var response = GlobalSetup.Client.GetMatchHistoryAsync(count: count)
                .Result;
            this.Sleep();

            Assert.True(response.Matches.Count == resultCount);
        }

        [Theory]
        [InlineData(78123870)]
        [InlineData(161273766)]
        [InlineData(386972442)]
        [InlineData(147169892)]
        public void AccountIdSpecified_ReturnsMatchesWithSpecifiedAccount(uint playerId)
        {
            var response = GlobalSetup.Client.GetMatchHistoryAsync(playerId32: playerId, count: 5)
                .Result;
            this.Sleep();

            foreach (var match in response.Matches)
            {
                Assert.Contains(match.Players, (player) => player.Id == playerId);
            }
        }

        [Theory]
        [InlineData(5609)]  // ESL One Hamburg 2017
        [InlineData(11517)] // DreamLeague Season 13
        [InlineData(10749)] // The International 2019
        [InlineData(9870)]  // The International 2018
        public void LeagueIdSpecified_ReturnsMatchesFromSpecifiedLeague(uint leagueId)
        {
            var response = GlobalSetup.Client.GetMatchHistoryAsync(leagueId: leagueId)
                .Result;
            this.Sleep();
            var details = GlobalSetup.Client.GetMatchDetailsAsync(
                response.Matches.ElementAt(0).MatchId.ToString()).Result;
            this.Sleep();

            Assert.Equal(leagueId.ToString(), details.LeagueId.ToString());
        }

        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(20)]
        public void MinPlayersSpecified_ReturnsMatchesWithMinimumPLayerCount(byte minPlayerCount)
        {
            var response = GlobalSetup.Client.GetMatchHistoryAsync(minPlayers: minPlayerCount)
                .Result;
            this.Sleep();

            foreach (var match in response.Matches)
            {
                Assert.True(match.Players.Count >= minPlayerCount);
            }
        }

        [Theory]
        [InlineData(80)]
        [InlineData(88)]
        [InlineData(36)]
        [InlineData(1)]
        [InlineData(66)]
        public void HeroSpecified_ReturnsOnlyGamesWithSPecifiedHero(ushort heroId)
        {
            var response = GlobalSetup.Client.GetMatchHistoryAsync(heroId: heroId, count: 2)
                .Result;
            this.Sleep();

            foreach (var match in response.Matches)
            {
                var players = from p in match.Players
                              where p.HeroId == heroId
                              select p;
                Assert.Contains(players, (player) => player.HeroId == heroId);
            }
        }
    }
}
