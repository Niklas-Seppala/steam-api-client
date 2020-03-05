using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Dota 2 MMR leaderboard model
    /// </summary>
    public class Leaderboard
    {
        /// <summary>
        /// Unixtimestamp of post
        /// </summary>
        [JsonProperty("time_posted")]
        public ulong TimePosted { get; set; }

        /// <summary>
        /// Unixtimestamp of the next post time
        /// </summary>
        [JsonProperty("next_scheduled_post_time")]
        public ulong NextScheduledPostTime { get; set; }

        /// <summary>
        /// Unixtimestamp of the server time
        /// </summary>
        [JsonProperty("server_time")]
        public ulong ServerTime { get; set; }

        /// <summary>
        /// List of dota 2 ranked players in the leaderboard
        /// </summary>
        [JsonProperty("leaderboard")]
        public IReadOnlyList<RankedPlayer> Players { get; set; }
    }
}
