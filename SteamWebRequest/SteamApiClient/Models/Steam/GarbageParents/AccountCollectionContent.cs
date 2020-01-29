using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    internal sealed class AccountCollectionContent
    {
        [JsonProperty("players")]
        public IReadOnlyCollection<SteamAccount> Accounts { get; set; }
    }
}
