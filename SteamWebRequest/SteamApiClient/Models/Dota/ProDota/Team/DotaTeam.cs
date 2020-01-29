using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
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
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeCreated { get; set; }

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
        public int GamesPlayedTotal { get; set; }

        [JsonProperty("games_played_matchmaking")]
        public int GamesPlayedMatchmaking { get; set; }

        [JsonProperty("registered_member_account_ids")]
        public IReadOnlyCollection<int> RegisteredAccountIds { get; set; }

        public IReadOnlyCollection<DotaTeamMember> Members { get; set; }

        [JsonProperty("audit_entries")]
        public IReadOnlyCollection<IReadOnlyDictionary<string, ulong>> AuditEntries { get; set; }

        public int Region { get; set; }

        [JsonProperty("url_logo")]
        public string UrlLogo { get; set; }

        [JsonProperty("dpc_results")]
        public DcpResultsCollection DCP { get; set; }
    }
}
