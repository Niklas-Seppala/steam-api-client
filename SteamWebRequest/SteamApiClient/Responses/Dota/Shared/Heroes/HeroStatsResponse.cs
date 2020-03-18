using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Web API response for hero stats request.
    /// </summary>
    [Serializable]
    public sealed class HeroStatsResponse : ApiResponse
    {
        /// <summary>
        /// Hero stats dictionary
        /// </summary>
        [JsonProperty("herodata")]
        public IReadOnlyDictionary<string, HeroStats> Contents { get; set; }
    }
}
