using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Pro dota 2 league model
    /// </summary>
    [Serializable]
    public sealed class League
    {
        /// <summary>
        /// Tournament URL
        /// </summary>
        [JsonProperty("tournament_url")]
        public string Url { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public uint ItemDef { get; set; }

        /// <summary>
        /// League name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// League id
        /// </summary>
        public uint LeagueId { get; set; }

        /// <summary>
        /// League description
        /// </summary>
        public string Description { get; set; }
    }
}
