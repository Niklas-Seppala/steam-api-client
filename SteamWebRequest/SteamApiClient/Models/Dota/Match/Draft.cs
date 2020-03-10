using Newtonsoft.Json;
using System;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Dota 2 match draft phase model
    /// </summary>
    [Serializable]
    public class Draft
    {
        /// <summary>
        /// Is drafted hero a pick
        /// </summary>
        [JsonProperty("is_pick")]
        public bool IsPick { get; set; }

        /// <summary>
        /// Id of the drafted hero
        /// </summary>
        [JsonProperty("hero_id")]
        public uint HeroId { get; set; }

        /// <summary>
        /// Which team drafted the hero
        /// </summary>
        public uint Team { get; set; }

        /// <summary>
        /// Draft order
        /// </summary>
        public uint Order { get; set; }
    }
}
