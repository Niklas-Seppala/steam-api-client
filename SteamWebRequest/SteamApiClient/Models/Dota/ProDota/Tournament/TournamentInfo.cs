using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Tournament info model
    /// </summary>
    public class TournamentInfo
    {
        /// <summary>
        /// League id
        /// </summary>
        [JsonProperty("league_id")]
        public ulong LeagueId { get; set; }

        /// <summary>
        /// Name of the tournament
        /// </summary>
        [JsonProperty("name")]
        public string TournamentName { get; set; }

        /// <summary>
        /// Pro tier
        /// </summary>
        public uint Tier { get; set; }

        /// <summary>
        /// Tournament region
        /// </summary>
        public uint Region { get; set; }

        /// <summary>
        /// Recent activity
        /// </summary>
        [JsonProperty("most_recent_activity")]
        public ulong MostRecentActivity { get; set; }

        /// <summary>
        /// Total tournament prize pool
        /// </summary>
        [JsonProperty("total_prize_pool")]
        public ulong PrizePool { get; set; }

        /// <summary>
        /// Unixtimestamp of the tournament's start datetime
        /// </summary>
        [JsonProperty("start_timestamp")]
        public ulong StartTimestamp { get; set; }

        /// <summary>
        /// Unixtimestamp of the tournament's end datetime
        /// </summary>
        [JsonProperty("end_timestamp")]
        public ulong EndTimestamp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint Status { get; set; }
    }
}
