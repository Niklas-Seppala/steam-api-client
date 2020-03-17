using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Realtime dota2 match
    /// </summary>
    [Serializable]
    public sealed class RealTimeMatch
    {
        /// <summary>
        /// Steam servers id that hosts the match
        /// </summary>
        public ulong ServerSteamId { get; set; }

        /// <summary>
        /// Match id
        /// </summary>
        public ulong MatchId { get; set; }

        /// <summary>
        /// Unixtimestamp
        /// </summary>
        public uint Timestamp { get; set; }

        /// <summary>
        /// Game time in seconds. Is negative
        /// before the match actually starts.
        /// </summary>
        [JsonProperty("game_time")]
        public int GameTime { get; set; }

        /// <summary>
        /// Game mode
        /// </summary>
        [JsonProperty("game_mode")]
        public uint GameMode { get; set; }

        /// <summary>
        /// League id. Optional
        /// </summary>
        [JsonProperty("league_id")]
        public uint LeagueId { get; set; }

        /// <summary>
        /// League node id. Optional
        /// </summary>
        [JsonProperty("league_node_id")]
        public uint LeagueNodeId { get; set; }

        /// <summary>
        /// Game state
        /// </summary>
        [JsonProperty("game_state")]
        public uint GameState { get; set; }
    }
}
