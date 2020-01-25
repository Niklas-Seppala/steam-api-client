using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public sealed class Friendslist
    {
        [JsonProperty("friendslist")]
        public FriendslistContent Content { get; set; }

        public List<Friend> Friends => this.Content.Friends;
    }
}
