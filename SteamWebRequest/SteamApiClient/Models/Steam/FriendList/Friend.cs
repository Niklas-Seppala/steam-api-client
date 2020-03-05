using Newtonsoft.Json;

namespace SteamApi.Models.Steam
{
    public partial class Friend
    {
        [JsonProperty("steamid")]
        public ulong Id64 { get; set; }
        [JsonProperty("friend_since")]
        public ulong FriendSince { get; set; }
    }
}
