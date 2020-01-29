using Newtonsoft.Json;
using System;

namespace SteamApiClient.Models.Dota
{
    public class RealTimeMatch
    {
        public ulong ServerSteamId { get; set; }
        public ulong MatchId { get; set; }
        public uint Timestamp { get; set; }

        [JsonProperty("game_time")]
        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan GameTime { get; set; }

        [JsonProperty("game_mode")]
        public byte GameMode { get; set; }

        [JsonProperty("league_id")]
        public uint LeagueId { get; set; }

        [JsonProperty("league_node_id")]
        public ushort LeagueNodeId { get; set; }

        [JsonProperty("game_state")]
        public byte GameState { get; set; }
    }
}
