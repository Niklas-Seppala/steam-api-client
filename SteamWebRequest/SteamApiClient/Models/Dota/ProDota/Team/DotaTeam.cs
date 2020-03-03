using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class DotaTeam
    {
        [JsonProperty("team_id")]
        public ulong TeamId { get; set; }
        public string Name { get; set; }
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }
        [JsonProperty("tag")]
        public string TeamTag { get; set; }
        [JsonProperty("time_created")]
        public ulong TimeCreated { get; set; }
        [JsonProperty("pro")]
        public bool IsProTeam { get; set; }
        public ulong UGC_Logo { get; set; }
        [JsonProperty("ugc_sponsor_logo")]
        public ulong UGC_SponsorLogo { get; set; }
        [JsonProperty("ugc_banner_logo")]
        public ulong UGC_BannerLogo { get; set; }
        [JsonProperty("ugc_base_logo")]
        public ulong UGC_BaseLogo { get; set; }
        public string URL { get; set; }
        public int Losses { get; set; }
        public int Wins { get; set; }
        [JsonProperty("games_played_total")]
        public uint GamesPlayedTotal { get; set; }
        [JsonProperty("games_played_matchmaking")]
        public uint GamesPlayedMatchmaking { get; set; }
        [JsonProperty("registered_member_account_ids")]
        public IReadOnlyList<int> RegisteredAccountIds { get; set; }
        public IReadOnlyList<DotaTeamMember> Members { get; set; }
        [JsonProperty("audit_entries")]
        public IReadOnlyList<IReadOnlyDictionary<string, ulong>> AuditEntries { get; set; }
        public uint Region { get; set; }
        [JsonProperty("url_logo")]
        public string UrlLogo { get; set; }
        [JsonProperty("dpc_results")]
        public DcpResultsCollection DCP { get; set; }
    }
}
