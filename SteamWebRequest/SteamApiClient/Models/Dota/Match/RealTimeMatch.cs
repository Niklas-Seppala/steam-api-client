using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class RealTimeMatch
    {
        public ulong ServerSteamId { get; set; }
        public ulong MatchId { get; set; }
        public uint Timestamp { get; set; }
        [JsonProperty("game_time")]
        public int GameTime { get; set; }
        [JsonProperty("game_mode")]
        public byte GameMode { get; set; }
        [JsonProperty("league_id")]
        public uint LeagueId { get; set; }
        [JsonProperty("league_node_id")]
        public uint LeagueNodeId { get; set; }
        [JsonProperty("game_state")]
        public byte GameState { get; set; }
    }
}
