using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamWebRequest
{
    public sealed class GameItems
    {
        [JsonProperty("result")]
        public ItemsContent Content { get; set; }

        public List<Item> Items => this.Content.Items;
    }

    public sealed class ItemsContent
    {
        [JsonProperty("items")]
        public List<Item> Items { get; set; }
    }

    public sealed class Item
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("cost")]
        public int Cost { get; set; }

        [JsonProperty("secret_shop")]
        public byte SecretShop { get; set; }

        [JsonProperty("side_shop")]
        public byte SideShop { get; set; }

        [JsonProperty("recipe")]
        public int Recipe { get; set; }
    }
}
