using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Dota 2 PLayers profile model
    /// </summary>
    public class DotaPlayerProfile
    {
        /// <summary>
        /// Player's team id
        /// </summary>
        [JsonProperty("team_id")]
        public ulong TeamId { get; set; }

        /// <summary>
        /// PLayer's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Player's country 
        /// </summary>
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Player's account id
        /// </summary>
        [JsonProperty("account_id")]
        public ulong AccountId { get; set; }

        /// <summary>
        /// Player's fantasy role (TI)
        /// </summary>
        [JsonProperty("fantasy_role")]
        public uint FantasyRole { get; set; }

        /// <summary>
        /// PLayer's team name
        /// </summary>
        [JsonProperty("team_name")]
        public string TeamName { get; set; }

        /// <summary>
        /// Player's team tag
        /// </summary>
        [JsonProperty("team_tag")]
        public string TeamTag { get; set; }

        /// <summary>
        /// PLayer's sponsor
        /// </summary>
        public string Sponsor { get; set; }

        /// <summary>
        /// Is player locked to his/her team
        /// </summary>
        [JsonProperty("is_locked")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// Is player pro
        /// </summary>
        [JsonProperty("is_pro")]
        public bool IsPro { get; set; }

        /// <summary>
        /// Player's total earnings
        /// </summary>
        [JsonProperty("total_earnings")]
        public ulong TotalEarnings { get; set; }

        /// <summary>
        /// PLayer's team's logo URL
        /// </summary>
        [JsonProperty("team_url_logo")]
        public string TeamLogoURL { get; set; }

        /// <summary>
        /// Player's real name
        /// </summary>
        [JsonProperty("real_name")]
        public string RealName { get; set; }

        /// <summary>
        /// Some kind of happenings
        /// </summary>
        [JsonProperty("audit_entries")]
        public IReadOnlyList<AuditEntry> AuditEntries { get; set; }
    }
}
