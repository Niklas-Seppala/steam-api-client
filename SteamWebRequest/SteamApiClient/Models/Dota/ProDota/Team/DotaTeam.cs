using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class DotaTeam
    {
        /// <summary>
        /// Team id
        /// </summary>
        [JsonProperty("team_id")]
        public ulong TeamId { get; set; }

        /// <summary>
        /// Team's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Teams country code
        /// </summary>
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Team's tag
        /// </summary>
        [JsonProperty("tag")]
        public string TeamTag { get; set; }

        /// <summary>
        /// Unixtimestamp of the date team was created
        /// </summary>
        [JsonProperty("time_created")]
        public ulong TimeCreated { get; set; }

        /// <summary>
        /// Is team listed as pro team
        /// </summary>
        [JsonProperty("pro")]
        public bool IsProTeam { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ulong UGC_Logo { get; set; }
        [JsonProperty("ugc_sponsor_logo")]
        public ulong UGC_SponsorLogo { get; set; }
        [JsonProperty("ugc_banner_logo")]
        public ulong UGC_BannerLogo { get; set; }
        [JsonProperty("ugc_base_logo")]
        public ulong UGC_BaseLogo { get; set; }

        /// <summary>
        /// Team URL
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Team's loss count
        /// </summary>
        public int Losses { get; set; }

        /// <summary>
        /// Team's win count
        /// </summary>
        public int Wins { get; set; }

        /// <summary>
        /// Teams total played games
        /// </summary>
        [JsonProperty("games_played_total")]
        public uint GamesPlayedTotal { get; set; }

        /// <summary>
        /// Games played in matchmaking
        /// </summary>
        [JsonProperty("games_played_matchmaking")]
        public uint GamesPlayedMatchmaking { get; set; }

        /// <summary>
        /// Registered account ids
        /// </summary>
        [JsonProperty("registered_member_account_ids")]
        public IReadOnlyList<int> RegisteredAccountIds { get; set; }

        /// <summary>
        /// List of team members
        /// </summary>
        public IReadOnlyList<DotaTeamMember> Members { get; set; }

        [JsonProperty("audit_entries")]
        public IReadOnlyList<IReadOnlyDictionary<string, ulong>> AuditEntries { get; set; }

        /// <summary>
        /// Team's region id
        /// </summary>
        public uint Region { get; set; }

        /// <summary>
        /// Logo URL
        /// </summary>
        [JsonProperty("url_logo")]
        public string UrlLogo { get; set; }

        /// <summary>
        /// DCP results so far this season
        /// </summary>
        [JsonProperty("dpc_results")]
        public DcpResultsCollection DCP { get; set; }
    }
}
