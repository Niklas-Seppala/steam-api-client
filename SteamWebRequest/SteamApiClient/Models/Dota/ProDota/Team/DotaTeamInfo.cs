using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SteamApiClient.Models.Dota
{
    public class DotaTeamInfo
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public ulong Logo { get; set; }

        [JsonProperty("time_created")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime TimeCreated { get; set; }

        [JsonProperty("logo_sponsor")]
        public ulong SponsorLogo { get; set; }

        [JsonProperty("country_code")]
        public string CountryCode { get; set; }

        [JsonProperty("games_played")]
        public uint GamesPlayed { get; set; }

        public string Url { get; set; }

        [JsonProperty("admin_account", NullValueHandling = NullValueHandling.Ignore)]
        public uint? AdminAccount { get; set; }

        [JsonProperty("player_0_account_id", NullValueHandling = NullValueHandling.Ignore)]
        public uint? Player_0_AccountId { get; set; }

        [JsonProperty("player_1_account_id", NullValueHandling = NullValueHandling.Ignore)]
        public uint? Player_1_AccountId { get; set; }

        [JsonProperty("player_2_account_id", NullValueHandling = NullValueHandling.Ignore)]
        public uint? Player_2_AccountId { get; set; }

        [JsonProperty("player_3_account_id", NullValueHandling = NullValueHandling.Ignore)]
        public uint? Player_3_AccountId { get; set; }

        [JsonProperty("player_4_account_id", NullValueHandling = NullValueHandling.Ignore)]
        public uint? Player_4_AccountId { get; set; }
    }
}
