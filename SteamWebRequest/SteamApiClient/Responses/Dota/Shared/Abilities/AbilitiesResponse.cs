using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response to abilities request.
    /// </summary>
    [Serializable]
    public sealed class AbilitiesResponse : ApiResponse
    {
        /// <summary>
        /// API response contents.
        /// </summary>
        [JsonProperty("abilitydata")]
        public IReadOnlyDictionary<string, Ability> Contents { get; set; }
    }
}
