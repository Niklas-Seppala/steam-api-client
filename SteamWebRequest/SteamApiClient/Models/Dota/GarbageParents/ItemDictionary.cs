using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    internal class ItemDictionary
    {
        [JsonProperty("itemdata")]
        public Dictionary<string, Item> ItemDict { get; set; }
    }
}
