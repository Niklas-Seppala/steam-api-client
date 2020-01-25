using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public sealed class AccountCollectionContent
    {
        [JsonProperty("players")]
        public List<SteamAccount> Accounts { get; set; }
    }
}
