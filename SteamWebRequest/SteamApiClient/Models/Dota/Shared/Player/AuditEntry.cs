using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class AuditEntry
    {
        [JsonProperty("start_timestamp")]
        public ulong StartTimeStamp { get; set; }
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
