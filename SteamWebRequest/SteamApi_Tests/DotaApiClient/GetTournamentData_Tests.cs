using Xunit;

namespace Client
{
    public class GetTournamentData_Tests : SteamApiClientTests
    {
        public GetTournamentData_Tests(ClientFixture fixture) : base(fixture) { }

        [Theory]
        [InlineData(9870, 25532177)]
        [InlineData(11517, 1000000)]
        public void GetTournamentPrizePool_LeagueIdProvided_ReturnsCorrectPrizepool(
            uint leagueId, uint prize)
        {
            var resp = Client.GetTournamentPrizePoolAsync(leagueId)
                .Result;
            SleepAfterApiCall();

            Assert.Equal(prize, resp["prize_pool"]);
            Assert.Equal(leagueId, resp["league_id"]);
        }

        [Theory]
        [InlineData(72312627, 9870)]  // TI8
        [InlineData(26771994, 10749)] // TI9
        [InlineData(26771994, 9870)]  // TI8
        public void GetTournamentPlayerStats_ProPlayerIdProvided_ReturnsMatchesFromEventByPlayer
            (uint playerId, uint eventId)
        {
            var eventMatches = Client.GetTournamentPlayerStatsAsync(playerId, eventId)
                .Result;
            SleepAfterApiCall();

            Assert.Equal(playerId, eventMatches.AccountId);
            Assert.NotEmpty(eventMatches.Matches);
            Assert.NotEmpty(eventMatches.HeroesPlayed);
        }

        [Theory]
        [InlineData(72312627, 9870, 42)]  // Wraith King
        [InlineData(26771994, 9870, 7)]   // Earthshaker
        [InlineData(19672354, 10749, 66)] // Chen
        public void GetTournamentPlayerStats_HeroIdProvided_ReturnsMatchesWithSpecifiedHero(
            uint playerId, uint eventId, ushort heroId)
        {
            var eventMatches = Client.GetTournamentPlayerStatsAsync(playerId, eventId,
                 heroId: heroId).Result;
            SleepAfterApiCall();

            Assert.Equal(playerId, eventMatches.AccountId);
            Assert.NotEmpty(eventMatches.Matches);
            Assert.NotEmpty(eventMatches.HeroesPlayed);
            Assert.All(eventMatches.Matches, (match) => Assert.Equal(heroId, match.HeroId));
        }

        [Fact]
        public void GetTournamentPlayerStats_NonProId_ReturnsEmptyObject()
        {
            var eventMatches = Client.GetTournamentPlayerStatsAsync(147169892, 9870)
                .Result;
            SleepAfterApiCall();

            Assert.True(eventMatches.AccountId == 0);
            Assert.Empty(eventMatches.Matches);
            Assert.Null(eventMatches.HeroesPlayed);
        }

        [Fact]
        public void GetTopLiveGames_DefaultParams_ReturnsTopLiveGames()
        {
            var topGames = Client.GetTopLiveGamesAsync()
                .Result;
            SleepAfterApiCall();

            Assert.NotEmpty(topGames);
        }

        [Fact(Skip = "Event games are hard to come by.")]
        public void GetTopLiveEventGames_DefaultParams_ReturnsTopLiveEventGames()
        {
            var topEventGames = Client.GetTopLiveEventGamesAsync()
                .Result;

            Assert.NotNull(topEventGames);
            Assert.NotEmpty(topEventGames);
        }

        [Theory]
        [InlineData(5, 5)]
        [InlineData(20, 20)]
        [InlineData(50, 50)]
        [InlineData(150, 100)]
        [InlineData(200, 100)]
        public void GetDotaTeamInfosById_CountProvided_ReturnsCorrectAmount(
            byte count, byte resultCount)
        {
            var teams = Client.GetDotaTeamInfosByIdAsync(count: count)
                .Result;
            SleepAfterApiCall();

            Assert.Equal(resultCount, teams.Count);
        }

        [Fact]
        public void GetLiveLeagueMatch_DefaultParameters_ReturnsLiveLeagueGames()
        {
            var leagueGames = Client.GetLiveLeagueMatchAsync()
                .Result;

            Assert.NotEmpty(leagueGames);
        }
    }
}
