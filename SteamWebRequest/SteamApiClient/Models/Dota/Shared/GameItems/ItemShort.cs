using Newtonsoft.Json;
using System;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Light version of the Item model
    /// </summary>
    [Serializable]
    public sealed class ItemShort
    {
        /// <summary>
        /// Localized name
        /// </summary>
        [JsonProperty("Localized_name")]
        public string LocalizedName { get; set; }

        /// <summary>
        /// Item id
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Item cost from shop
        /// </summary>
        public uint Cost { get; set; }

        /// <summary>
        /// Item name
        /// </summary>
        public string Name { get; set; }
    }
}
