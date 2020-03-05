using Newtonsoft.Json;

namespace SteamApi.Models.Steam
{
    /// <summary>
    /// Basic Steam server info
    /// </summary>
    public sealed class SteamServerInfo
    {
        /// <summary>
        /// Unixtimestamp of servertime
        /// </summary>
        [JsonProperty("servertime")]
        public ulong ServerTime { get; set; }

        /// <summary>
        /// Local server time as string
        /// </summary>
        [JsonProperty("servertimestring")]
        public string ServerLocalTime { get; set; }
    }
}
