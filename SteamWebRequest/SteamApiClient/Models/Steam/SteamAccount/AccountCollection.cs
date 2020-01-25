using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public sealed class AccountCollection
    {
        [JsonProperty("response")]
        public AccountCollectionContent Content { get; set; }
        public List<SteamAccount> Accounts => this.Content.Accounts;
    }
}
