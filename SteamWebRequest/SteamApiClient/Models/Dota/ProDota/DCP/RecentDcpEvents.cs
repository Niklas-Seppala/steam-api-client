using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class RecentDcpEvents
    {
        public IReadOnlyList<Tournament> Tournaments { get; set; }
        [JsonProperty("wager_timestamp")]
        public ulong WagerTimestamp { get; set; }
    }
}
