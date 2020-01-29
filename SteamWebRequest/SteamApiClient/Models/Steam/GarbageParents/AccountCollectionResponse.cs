using Newtonsoft.Json;

namespace SteamApiClient.Models
{
    internal sealed class AccountCollectionResponse
    {
        [JsonProperty("response")]
        public AccountCollectionContent Content { get; set; }
    }
}
