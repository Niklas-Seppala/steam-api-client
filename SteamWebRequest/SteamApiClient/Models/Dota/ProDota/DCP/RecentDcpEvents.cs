using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    public class RecentDcpEvents
    {
        public IReadOnlyCollection<Tournament> Tournaments { get; set; }

        [JsonProperty("wager_timestamp")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime WagerTimeStamp { get; set; }
    }
}
