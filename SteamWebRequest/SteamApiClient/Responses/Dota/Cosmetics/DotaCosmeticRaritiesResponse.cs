using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response to Cosmetic rarities request.
    /// </summary>
    [Serializable]
    public sealed class CosmeticRaritiesResponse : ApiResponse
    {
        /// <summary>
        /// API response contents.
        /// </summary>
        [JsonProperty("rarities")]
        public IReadOnlyList<CosmeticRarity> Contents { get; set; }
    }
}
