using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Dota 2 cosmetic rarity model class
    /// </summary>
    [Serializable]
    public sealed class DotaCosmeticRarity
    {
        /// <summary>
        /// Rarity name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Rarity id
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// rarity order
        /// </summary>
        public uint Order { get; set; }

        /// <summary>
        /// Rarity color
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Localized name for the rarity
        /// </summary>
        [JsonProperty("localized_name")]
        public string LocalizedName { get; set; }
    }
}
