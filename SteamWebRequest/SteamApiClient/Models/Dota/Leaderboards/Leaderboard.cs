using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class Leaderboard
    {
        [JsonProperty("time_posted")]
        public ulong TimePosted { get; set; }

        [JsonProperty("next_scheduled_post_time")]
        public ulong NextScheduledPostTime { get; set; }

        [JsonProperty("server_time")]
        public ulong ServerTime { get; set; }

        [JsonProperty("leaderboard")]
        public IReadOnlyList<RankedPlayer> Players { get; set; }
    }
}
