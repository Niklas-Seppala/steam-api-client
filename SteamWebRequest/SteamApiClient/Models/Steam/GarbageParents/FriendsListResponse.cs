using Newtonsoft.Json;

namespace SteamApi.Models
{
    internal class FriendslistResponse
    {
        [JsonProperty("friendslist")]
        public FriendslistContent Content { get; set; }
    }
}
