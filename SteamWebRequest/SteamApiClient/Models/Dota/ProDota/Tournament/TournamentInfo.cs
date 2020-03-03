using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class TournamentInfo
    {
        [JsonProperty("league_id")]
        public ulong LeagueId { get; set; }
        [JsonProperty("name")]
        public string TournamentName { get; set; }
        public uint Tier { get; set; }
        public uint Region { get; set; }
        [JsonProperty("most_recent_activity")]
        public ulong MostRecentActivity { get; set; }
        [JsonProperty("total_prize_pool")]
        public ulong PrizePool { get; set; }
        [JsonProperty("start_timestamp")]
        public ulong StartTimestamp { get; set; }
        [JsonProperty("end_timestamp")]
        public ulong EndTimestamp { get; set; }
        public uint Status { get; set; }
    }
}
