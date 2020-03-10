using Newtonsoft.Json;
using System;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Draft phase pick event
    /// </summary>
    [Serializable]
    public class Pick
    {
        /// <summary>
        /// Hero id that this event targets
        /// </summary>
        [JsonProperty("hero_id")]
        public uint HeroId { get; set; }
    }
}
