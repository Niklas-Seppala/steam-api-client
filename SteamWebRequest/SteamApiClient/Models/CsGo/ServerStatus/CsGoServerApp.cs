using Newtonsoft.Json;

namespace SteamApi.Models.CsGo
{
    /// <summary>
    /// CS GO server status server app model
    /// </summary>
    public class CsGoServerApp
    {
        /// <summary>
        /// App version
        /// </summary>
        public uint Version { get; set; }

        /// <summary>
        /// Unixtimestamp
        /// </summary>
        public ulong Timestamp { get; set; }

        /// <summary>
        /// Time as a string
        /// </summary>
        [JsonProperty("time")]
        public string TimeAsText { get; set; }
    }
}