using SteamApiClient;
using SteamApiClient.Dota;
using SteamApiClient.Models.Dota;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Linq;
using System.Threading;

namespace SWR.Client_Tests
{
    public class GetMatchHistory_Tests
    {
        private readonly SteamHttpClient _client;

        // Not not bombard api with requests
        private readonly bool _sleepAfterTest;
        private int _sleepTimeMs = 400;

        public GetMatchHistory_Tests()
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
        [InlineData(50, 50)]
        [InlineData(100, 100)]
        [InlineData(200, 100)]
        [InlineData(5, 5)]
        [InlineData(1,1)]
        [InlineData(0, 0)]
        public void CountParamDefined_ReturnsSpecifiedAmount(byte count, byte resultCount)
        {
            var response = _client.GetMatchHistoryAsync(count: count)
                .Result;

            Assert.True(response.Matches.Count == resultCount);

            this.SleepAfterApiCall();
        }

        [Theory]
        [InlineData(78123870)]
        [InlineData(161273766)]
        [InlineData(386972442)]
        [InlineData(147169892)]
        public void AccountIdSpecified_ReturnsMatchesWithSpecifiedAccount(uint playerId)
        {
            var response = _client.GetMatchHistoryAsync(playerId32: playerId, count: 5)
                .Result;

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
            var response = _client.GetMatchHistoryAsync(leagueId: leagueId) 
                .Result;
            this.SleepAfterApiCall();
            var details = _client.GetMatchDetailsAsync(response.Matches.ElementAt(0).MatchId.ToString())
                .Result;

            Assert.Equal(leagueId.ToString(), details.LeagueId.ToString());

            this.SleepAfterApiCall();
        }

        [Theory]
        [InlineData(2)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(15)]
        [InlineData(20)]
        public void MinPlayersSpecified_ReturnsMatchesWithMinimumPLayerCount(byte minPlayerCount)
        {
            var response = _client.GetMatchHistoryAsync(minPlayers: minPlayerCount)
                .Result;

            foreach (var match in response.Matches)
            {
                Assert.True(match.Players.Count >= minPlayerCount);
            }

            this.SleepAfterApiCall();
        }

        [Theory]
        [InlineData(80)]
        [InlineData(88)]
        [InlineData(36)]
        [InlineData(1)]
        [InlineData(66)]
        public void HeroSpecified_ReturnsOnlyGamesWithSPecifiedHero(ushort heroId)
        {
            var response = _client.GetMatchHistoryAsync(heroId: heroId, count: 2)
                .Result;
            SleepAfterApiCall();

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
