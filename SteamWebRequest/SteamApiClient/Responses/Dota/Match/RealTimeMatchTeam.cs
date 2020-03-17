using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Realtime dota 2 match team
    /// </summary>
    [Serializable]
    public sealed class RealTimeMatchTeam
    {
        /// <summary>
        /// Number of the team
        /// </summary>
        [JsonProperty("team_number")]
        public uint TeamNumber { get; set; }

        /// <summary>
        /// Id of the team
        /// </summary>
        [JsonProperty("team_id")]
        public uint TeamId { get; set; }

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
        /// Team logo id
        /// </summary>
        [JsonProperty("team_logo")]
        public ulong TeamLogo { get; set; }

        /// <summary>
        /// Current team score
        /// </summary>
        public uint Score { get; set; }

        /// <summary>
        /// Current team networth
        /// </summary>
        [JsonProperty("net_worth")]
        public uint NetWorth { get; set; }

        /// <summary>
        /// Team logo URL
        /// </summary>
        [JsonProperty("team_logo_url")]
        public string TeamLogoUrl { get; set; }

        /// <summary>
        /// List of the players in the team
        /// </summary>
        public IReadOnlyList<RealTimeMatchPlayer> Players { get; set; }
    }
}
