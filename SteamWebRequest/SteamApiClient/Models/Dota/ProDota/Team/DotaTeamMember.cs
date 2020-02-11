using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SteamApi.Models.Dota
{
    public class DotaTeamMember
    {
        [JsonProperty("account_id")]
        public uint AccountId { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty("time_joined")]
        public DateTime TimeJoined { get; set; }

        public bool Admin { get; set; }
    }
}
