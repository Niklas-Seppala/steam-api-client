using Newtonsoft.Json;

namespace SteamApi.Models.Steam
{
    /// <summary>
    /// Steam Friend model.
    /// </summary>
    public sealed class Friend
    {
        /// <summary>
        /// 64-bit Steam id
        /// </summary>
        [JsonProperty("steamid")]
        public ulong Id64 { get; set; }

        /// <summary>
        /// Unixtimestamp
        /// </summary>
        [JsonProperty("friend_since")]
        public ulong FriendSince { get; set; }
    }
}
