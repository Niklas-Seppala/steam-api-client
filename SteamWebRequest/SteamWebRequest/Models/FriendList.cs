using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SteamWebRequest
{
    public sealed class Friendslist
    {
        [JsonProperty("friendslist")]
        public FriendslistContent Content { get; set; }

        public List<Friend> Friends { get => this.Content.Friends; }
    }

    public sealed class FriendslistContent
    {
        public List<Friend> Friends { get; set; }
    }

    public partial class Friend
    {
        [JsonProperty("steamid")]
        public string Id64 { get; set; }
        
        [JsonProperty("friend_since")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime FriendSince { get; set; }
    }
}
