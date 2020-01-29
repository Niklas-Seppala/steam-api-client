using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public class DotaPlayerProfile
    {
        [JsonProperty("team_id")]
        public ulong TeamId { get; set; }

        public string Name { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("account_id")]
        public ulong AccountId { get; set; }

        [JsonProperty("fantasy_role")]
        public uint FantasyRole { get; set; }

        [JsonProperty("team_name")]
        public string TeamName { get; set; }

        [JsonProperty("team_tag")]
        public string TeamTag { get; set; }

        public string Sponsor { get; set; }

        [JsonProperty("is_locked")]
        public bool IsLocked { get; set; }

        [JsonProperty("is_pro")]
        public bool IsPro { get; set; }

        [JsonProperty("total_earnings")]
        public ulong TotalEarnings { get; set; }

        [JsonProperty("team_url_logo")]
        public string TeamLogoURL { get; set; }

        [JsonProperty("real_name")]
        public string RealName { get; set; }

        [JsonProperty("audit_entries")]
        public IReadOnlyCollection<AuditEntry> AuditEntries { get; set; }
    }
}
