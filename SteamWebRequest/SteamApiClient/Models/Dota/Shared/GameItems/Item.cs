﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public sealed class Item
    {
        [JsonProperty("id")]
        public ushort Id { get; set; }

        [JsonProperty("img")]
        public string ImageName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("dname")]
        public string LocalizedName { get; set; }

        [JsonProperty("cd")]
        [JsonConverter(typeof(BoolsAndIntsToUshortConverter))]
        public ushort Cooldown { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("notes")]
        public string Notes { get; set; }

        [JsonProperty("lore")]
        public string Lore { get; set; }

        [JsonProperty("cost")]
        public int Cost { get; set; }

        [JsonProperty("secret_shop")]
        public bool SecretShop { get; set; }

        [JsonProperty("side_shop")]
        public bool SideShop { get; set; }

        [JsonProperty("recipe")]
        public int Recipe { get; set; }

        [JsonProperty("created")]
        public bool Created { get; set; }

        [JsonProperty("qual")]
        public string Quality { get; set; }

        [JsonProperty("attrib")]
        public string Attrributes { get; set; }
    }

    internal class ItemDictionary
    {
        [JsonProperty("itemdata")]
        public Dictionary<string, Item> ItemDict { get; set; }
    }
}