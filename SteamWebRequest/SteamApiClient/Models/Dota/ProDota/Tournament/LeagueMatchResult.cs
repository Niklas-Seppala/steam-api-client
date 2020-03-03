using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class LeagueMatchResult
    {
        [JsonProperty("match_id")]
        public ulong MatchId { get; set; }
        [JsonProperty("winning_team_id")]
        public ulong WinnerTeamId { get; set; }
    }
}
