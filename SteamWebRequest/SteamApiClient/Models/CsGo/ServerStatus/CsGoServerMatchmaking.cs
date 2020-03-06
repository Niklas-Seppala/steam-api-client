using Newtonsoft.Json;

namespace SteamApi.Models.CsGo
{
    /// <summary>
    /// CS GO server matchmaking model
    /// </summary>
    public class CsGoServerMatchmaking
    {
        /// <summary>
        /// Scheduler
        /// </summary>
        public string Scheduler { get; set; }

        /// <summary>
        /// How many servers are online
        /// </summary>
        [JsonProperty("online_servers")]
        public uint OnlineServerCount { get; set; }

        /// <summary>
        /// Online player count
        /// </summary>
        [JsonProperty("online_players")]
        public uint OnlinePlayerCount { get; set; }

        /// <summary>
        /// The amount of players who are searching for a
        /// match
        /// </summary>
        [JsonProperty("searching_players")]
        public uint PlayersSearchingMatch { get; set; }

        /// <summary>
        /// Current average time it takes to find a matchmaking
        /// game for a player
        /// </summary>
        [JsonProperty("search_seconds_avg")]
        public uint AverageMatchSearchTime { get; set; }
    }
}