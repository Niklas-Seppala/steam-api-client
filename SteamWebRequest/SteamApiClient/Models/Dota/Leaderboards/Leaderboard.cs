using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public class Leaderboard
    {
        [JsonProperty("time_posted")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimePosted { get; set; }

        [JsonProperty("next_scheduled_post_time")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime Next_ScheduledPostTime { get; set; }

        [JsonProperty("server_time")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime ServerTime { get; set; }

        [JsonProperty("leaderboard")]
        public List<RankedPlayer> Players { get; set; }
    }
}
