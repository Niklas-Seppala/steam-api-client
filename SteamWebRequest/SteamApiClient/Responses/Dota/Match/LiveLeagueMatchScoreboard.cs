using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Live league match scoreboard model
    /// </summary>
    [Serializable]
    public sealed class LiveLeagueMatchScoreboard
    {
        /// <summary>
        /// Match duration
        /// </summary>
        public double Duration { get; set; }

        /// <summary>
        /// Timer until next Roshan respawn
        /// </summary>
        [JsonProperty("roshan_respawn_timer")]
        public uint RoshanRespawnTimer { get; set; }

        /// <summary>
        /// Dire team side of the scoreboard
        /// </summary>
        public LiveLeagueMatchScoreboardTeam Dire { get; set; }

        /// <summary>
        /// Radiant team side of the scoreboard
        /// </summary>
        public LiveLeagueMatchScoreboardTeam Radiant { get; set; }
    }
}
