using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public sealed class GameItems
    {
        [JsonProperty("result")]
        public ItemsContent Content { get; set; }

        public List<Item> Items => this.Content.Items;
    }
}
