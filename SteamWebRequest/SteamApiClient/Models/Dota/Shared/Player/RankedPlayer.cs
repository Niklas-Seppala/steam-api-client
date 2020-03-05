using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Dota 2 ranked player model
    /// </summary>
    public class RankedPlayer
    {
        /// <summary>
        /// Team id
        /// </summary>
        [JsonProperty("team_id")]
        public ulong TeamId { get; set; }

        /// <summary>
        /// Team tag
        /// </summary>
        [JsonProperty("team_tag")]
        public string TeamTag { get; set; }

        /// <summary>
        /// Sponsor
        /// </summary>
        public string Sponsor { get; set; }

        /// <summary>
        /// Country code
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Rank
        /// </summary>
        public uint Rank { get; set; }

        /// <summary>
        /// PLayer name
        /// </summary>
        public string Name { get; set; }
    }
}
