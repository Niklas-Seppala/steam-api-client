using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Web API response to heroes request.
    /// </summary>
    [Serializable]
    public sealed class HeroesResponse : ApiResponse
    {
        /// <summary>
        /// List of heroes
        /// </summary>
        [JsonProperty("heroes")]
        public IReadOnlyList<Hero> Contents { get; set; }
    }
}
