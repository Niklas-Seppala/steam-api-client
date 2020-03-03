using Newtonsoft.Json;

namespace SteamApi.Models
{
    internal sealed class FriendslistResponse
    {
        [JsonProperty("friendslist")]
        public FriendslistContent Content { get; set; }
    }
}
