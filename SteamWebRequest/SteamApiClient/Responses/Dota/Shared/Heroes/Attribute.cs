using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Dota 2 Hero's attribute
    /// </summary>
    [Serializable]
    public sealed class Attribute
    {
        /// <summary>
        /// Start value
        /// </summary>
        [JsonProperty("b")]
        public uint Base { get; set; }

        /// <summary>
        /// Gained each level
        /// </summary>
        [JsonProperty("g")]
        public float Gain { get; set; }
    }
}
