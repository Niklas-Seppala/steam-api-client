using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SteamApi.Models
{
    public partial class Friend
    {
        [JsonProperty("steamid")]
        public string Id64 { get; set; }

        [JsonProperty("friend_since")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime FriendSince { get; set; }
    }
}
