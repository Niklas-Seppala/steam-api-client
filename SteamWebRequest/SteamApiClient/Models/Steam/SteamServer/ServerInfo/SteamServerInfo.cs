using Newtonsoft.Json;

namespace SteamApi.Models.Steam
{
    public sealed class SteamServerInfo
    {
        [JsonProperty("servertime")]
        public ulong ServerTime { get; set; }
        [JsonProperty("servertimestring")]
        public string ServerLocalTime { get; set; }
    }
}
