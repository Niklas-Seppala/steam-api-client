using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    internal class ItemsContent
    {
        [JsonProperty("items")]
        public IReadOnlyList<Item> Items { get; set; }
    }
}
