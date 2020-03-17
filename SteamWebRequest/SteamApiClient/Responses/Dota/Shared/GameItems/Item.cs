using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Game item model
    /// </summary>
    [Serializable]
    public sealed class Item
    {
        /// <summary>
        /// Game item id
        /// </summary>
        [JsonProperty("id")]
        public uint Id { get; set; }

        /// <summary>
        /// Game item image name
        /// </summary>
        [JsonProperty("img")]
        public string ImageName { get; set; }

        /// <summary>
        /// Game item name
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        
        [JsonProperty("dname")]
        public string Dname { get; set; }

        /// <summary>
        /// Game item localized name
        /// </summary>
        [JsonProperty("localized_name")]
        public string LocalizedName { get; set; }

        /// <summary>
        /// Cooldown time
        /// </summary>
        [JsonProperty("cd")]
        [JsonConverter(typeof(BoolToIntConverter))]
        public uint Cooldown { get; set; }

        /// <summary>
        /// Item description
        /// </summary>
        [JsonProperty("desc")]
        public string Description { get; set; }

        /// <summary>
        /// Item notes
        /// </summary>
        [JsonProperty("notes")]
        public string Notes { get; set; }

        /// <summary>
        /// item lore
        /// </summary>
        [JsonProperty("lore")]
        public string Lore { get; set; }

        /// <summary>
        /// item cost
        /// </summary>
        [JsonProperty("cost")]
        public uint Cost { get; set; }

        /// <summary>
        /// Bought from secret shop
        /// </summary>
        [JsonProperty("secret_shop")]
        public bool SecretShop { get; set; }

        /// <summary>
        /// Bought from side shop
        /// </summary>
        [JsonProperty("side_shop")]
        public bool SideShop { get; set; }

        /// <summary>
        /// Recipe id
        /// </summary>
        [JsonProperty("recipe")]
        public uint Recipe { get; set; }

        /// <summary>
        /// Is item created from multiple items
        /// </summary>
        [JsonProperty("created")]
        public bool Created { get; set; }

        /// <summary>
        /// Item Quality
        /// </summary>
        [JsonProperty("qual")]
        public string Quality { get; set; }

        /// <summary>
        /// Item attributes
        /// </summary>
        [JsonProperty("attrib")]
        public string Attrributes { get; set; }
    }
}
