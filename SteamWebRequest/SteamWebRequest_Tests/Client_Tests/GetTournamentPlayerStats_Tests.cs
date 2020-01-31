using SteamApiClient;
using SteamApiClient.Dota;
using System;
using Xunit;

namespace SWR.Client_Tests
{
    public class GetTournamentPlayerStats_Tests : SteamHttpClient_Tests
    {
        [Theory]
        [InlineData(72312627, 9870)]  // TI8
        [InlineData(26771994, 10749)] // TI9
        [InlineData(26771994, 9870)]  // TI8
        public void ProPlayerIdProvided_ReturnsMatchesFromEventByPlayer(uint playerId,
            uint eventId)
        {
            var eventMatches = _client.GetTournamentPlayerStatsAsync(playerId, eventId)
                .Result;
            this.Sleep();

            Assert.Equal(playerId, eventMatches.AccountId);
            Assert.NotEmpty(eventMatches.Matches);
            Assert.NotEmpty(eventMatches.HeroesPlayed);
        }

        [Theory]
        [InlineData(72312627, 9870, 42)]  // Wraith King
        [InlineData(26771994, 9870, 7)]   // Earthshaker
        [InlineData(19672354, 10749, 66)] // Chen
        public void HeroIdProvided_ReturnsMatchesWithSpecifiedHero(uint playerId,
            uint eventId, ushort heroId)
        {
            var eventMatches = _client.GetTournamentPlayerStatsAsync(playerId, eventId,
                 heroId: heroId).Result;
            this.Sleep();

            Assert.Equal(playerId, eventMatches.AccountId);
            Assert.NotEmpty(eventMatches.Matches);
            Assert.NotEmpty(eventMatches.HeroesPlayed);

            Assert.All(eventMatches.Matches, (match) => Assert.Equal(heroId, match.HeroId));
        }

        [Fact]
        public void NonProId_ReturnsEmptyObject()
        {
            var eventMatches = _client.GetTournamentPlayerStatsAsync(147169892, 9870)
                .Result;
            this.Sleep();

            Assert.True(eventMatches.AccountId == 0);
            Assert.Empty(eventMatches.Matches);
            Assert.Null(eventMatches.HeroesPlayed);
        }
    }
}
