using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response to item creators request.
    /// </summary>
    [Serializable]
    public sealed class ItemCreatorsResponse : ApiResponse
    {
        /// <summary>
        /// API response contentes
        /// </summary>
        [JsonProperty("Contributors")]
        public IReadOnlyList<uint> Contents { get; set; }
    }
}
