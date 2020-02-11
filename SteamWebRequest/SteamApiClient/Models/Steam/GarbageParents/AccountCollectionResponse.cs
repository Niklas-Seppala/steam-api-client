using Newtonsoft.Json;

namespace SteamApi.Models
{
    internal sealed class AccountCollectionResponse
    {
        [JsonProperty("response")]
        public AccountCollectionContent Content { get; set; }
    }
}
