using Newtonsoft.Json;

namespace SteamApiClient.Models
{
    internal class FriendslistResponse
    {
        [JsonProperty("friendslist")]
        public FriendslistContent Content { get; set; }
    }
}
