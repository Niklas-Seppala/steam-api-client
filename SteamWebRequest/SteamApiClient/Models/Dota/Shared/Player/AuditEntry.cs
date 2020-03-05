using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Somekind of pro dota team happening
    /// </summary>
    public class AuditEntry
    {
        /// <summary>
        /// Unixtimestamp
        /// </summary>
        [JsonProperty("start_timestamp")]
        public ulong StartTimeStamp { get; set; }

        /// <summary>
        /// Teams id
        /// </summary>
        [JsonProperty("team_id")]
        public ulong TeamId { get; set; }

        /// <summary>
        /// Name of the team
        /// </summary>
        [JsonProperty("team_name")]
        public string TeamName { get; set; }

        /// <summary>
        /// Team tag
        /// </summary>
        [JsonProperty("team_tag")]
        public string TeamTag { get; set; }

        /// <summary>
        /// Team's logo URL
        /// </summary>
        [JsonProperty("team_url_logo")]
        public string TeamLogoURL { get; set; }
    }
}
