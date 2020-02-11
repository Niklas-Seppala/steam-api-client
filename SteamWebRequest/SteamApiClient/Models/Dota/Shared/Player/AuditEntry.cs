using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SteamApi.Models.Dota
{
    public class AuditEntry
    {
        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty("start_timestamp")]
        public DateTime StartTimeStamp { get; set; }

        [JsonProperty("team_id")]
        public ulong TeamId { get; set; }

        [JsonProperty("team_name")]
        public string TeamName { get; set; }

        [JsonProperty("team_tag")]
        public string TeamTag { get; set; }

        [JsonProperty("team_url_logo")]
        public string TeamLogoURL { get; set; }
    }
}
