using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public sealed class ItemsContent
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }
}
