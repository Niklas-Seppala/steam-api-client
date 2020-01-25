using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SteamApiClient.Models
{
    public sealed class SteamServerInfo
    {
        [JsonProperty("servertime")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime ServerTime { get; set; }

        [JsonProperty("servertimestring")]
        public string ServerLocalTime { get; set; }
    }
}
