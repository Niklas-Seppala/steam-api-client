using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    internal class ItemsContent
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }
}
