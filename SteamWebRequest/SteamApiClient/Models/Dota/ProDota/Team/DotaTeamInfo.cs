using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Dota 2 team info model
    /// </summary>
    public class DotaTeamInfo
    {
        /// <summary>
        /// Team name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Team tag
        /// </summary>
        public string Tag { get; set; }

        /// <summary>
        /// Team logo id
        /// </summary>
        public ulong Logo { get; set; }

        /// <summary>
        /// Unixtimestamp of date when team was created
        /// </summary>
        [JsonProperty("time_created")]
        public ulong TimeCreated { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("logo_sponsor")]
        public ulong SponsorLogo { get; set; }

        /// <summary>
        /// Teams country
        /// </summary>
        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        /// <summary>
        /// Total count of games played
        /// </summary>
        [JsonProperty("games_played")]
        public uint GamesPlayed { get; set; }

        /// <summary>
        /// Team's URL
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Team's admins account id
        /// Can be null
        /// </summary>
        [JsonProperty("admin_account", NullValueHandling = NullValueHandling.Ignore)]
        public uint? AdminAccount { get; set; }

        /// <summary>
        /// Team's player slot 0 account id
        /// Can be null
        /// </summary>
        [JsonProperty("player_0_account_id", NullValueHandling = NullValueHandling.Ignore)]
        public uint? Player_0_AccountId { get; set; }

        /// <summary>
        /// Team's player slot 1 account id
        /// Can be null
        /// </summary>
        [JsonProperty("player_1_account_id", NullValueHandling = NullValueHandling.Ignore)]
        public uint? Player_1_AccountId { get; set; }

        /// <summary>
        /// Team's player slot 2 account id
        /// Can be null
        /// </summary>
        [JsonProperty("player_2_account_id", NullValueHandling = NullValueHandling.Ignore)]
        public uint? Player_2_AccountId { get; set; }

        /// <summary>
        /// Team's player slot 3 account id
        /// Can be null
        /// </summary>
        [JsonProperty("player_3_account_id", NullValueHandling = NullValueHandling.Ignore)]
        public uint? Player_3_AccountId { get; set; }

        /// <summary>
        /// Team's player slot 4 account id
        /// Can be null
        /// </summary>
        [JsonProperty("player_4_account_id", NullValueHandling = NullValueHandling.Ignore)]
        public uint? Player_4_AccountId { get; set; }
    }
}
