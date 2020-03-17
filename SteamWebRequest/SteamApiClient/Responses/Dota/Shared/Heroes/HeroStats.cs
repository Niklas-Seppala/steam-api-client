using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Dota 2 hero stats model
    /// </summary>
    [Serializable]
    public sealed class HeroStats
    {
        /// <summary>
        /// Localized name
        /// </summary>
        [JsonProperty("dname")]
        public string LocalizedName { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        [JsonProperty("u")]
        public string Name { get; set; }

        /// <summary>
        /// Primary attribute
        /// </summary>
        [JsonProperty("pa")]
        public string PrimaryAttribute { get; set; }

        /// <summary>
        /// Attributes
        /// </summary>
        [JsonProperty("attribs")]
        public Attributes Attributes { get; set; }
    }
}
