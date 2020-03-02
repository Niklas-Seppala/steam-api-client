using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

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
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime StartTimestamp { get; set; }

        [JsonProperty("end_timestamp")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime EndTimestamp { get; set; }

        public uint Status { get; set; }
    }
}
