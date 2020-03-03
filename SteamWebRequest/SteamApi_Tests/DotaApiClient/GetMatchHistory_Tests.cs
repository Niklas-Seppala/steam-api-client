using System.Linq;
using Xunit;

namespace Client
{
    public class GetMatchHistory_Tests : SteamApiClientTests
    {
        public GetMatchHistory_Tests(ClientFixture fixture) : base(fixture)
        { }

        [Theory]
        [InlineData(50, 50)]
        [InlineData(100, 100)]
        [InlineData(200, 100)]
        [InlineData(5, 5)]
        [InlineData(1, 1)]
        [InlineData(0, 0)]
        public void CountParamDefined_ReturnsSpecifiedAmount(byte count, byte resultCount)
        {
            var response = Client.GetMatchHistoryAsync(count: count)
                .Result;
            SleepAfterApiCall();

            Assert.True(response.Matches.Count == resultCount);
        }

        [Theory]
        [InlineData(78123870)]
        [InlineData(161273766)]
        [InlineData(386972442)]
        [InlineData(147169892)]
        public void AccountIdSpecified_ReturnsMatchesWithSpecifiedAccount(uint playerId)
        {
            var response = Client.GetMatchHistoryAsync(playerId32: playerId, count: 5)
                .Result;
            SleepAfterApiCall();

            foreach (var match in response.Matches)
            {
                Assert.Contains(match.Players, (player) => player.Id32 == playerId);
            }
        }

        [Theory]
        [InlineData(5609)]  // ESL One Hamburg 2017
        [InlineData(11517)] // DreamLeague Season 13
        [InlineData(10749)] // The International 2019
        [InlineData(9870)]  // The International 2018
        public void LeagueIdSpecified_ReturnsMatchesFromSpecifiedLeague(uint leagueId)
        {
            var response = Client.GetMatchHistoryAsync(leagueId: leagueId)
                .Result;
            SleepAfterApiCall();
            var details = Client.GetMatchDetailsAsync(
                response.Matches.ElementAt(0).MatchId.ToString()).Result;
            SleepAfterApiCall();

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
            var response = Client.GetMatchHistoryAsync(minPlayers: minPlayerCount)
                .Result;
            SleepAfterApiCall();

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
            var response = Client.GetMatchHistoryAsync(heroId: heroId, count: 2)
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

        [Theory]
        [InlineData(666666, 666667)]
        [InlineData(808089, 808089)]
        [InlineData(400, 401)]
        [InlineData(100, 240)] // starts at 240 for some reason
        [InlineData(0, 240)]
        public void GetMatchHistoryBySequenceNum_ValidSeqNum_ReturnsMatchesStartingFromSeqNum(
            ulong seqNum, ulong resultStartSeqNum)
        {
            var matches = Client.GetMatchHistoryBySequenceNumAsync(seqNum: seqNum, count: 1)
                .Result;
            SleepAfterApiCall();

            Assert.Equal(resultStartSeqNum, matches.ElementAt(0).MatchSequenceNum);
        }

        [Theory]
        [InlineData(5, 5)]
        [InlineData(25, 25)]
        [InlineData(200, 100)]
        [InlineData(60, 60)]
        [InlineData(1, 1)]
        public void GetMatchHistoryBySequenceNum_CountDefined_ReturnsCorrectAmountOfGames(
            byte count, byte actualCount)
        {
            ulong seqNum = 55555050;
            var matches = Client.GetMatchHistoryBySequenceNumAsync(seqNum, count: count)
                .Result;
            SleepAfterApiCall();

            Assert.Equal(actualCount, matches.Count);
        }
    }
}
