using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response for dota 2 items request.
    /// </summary>
    [Serializable]
    public class ItemsResponse : ApiResponse
    {
        /// <summary>
        /// API response contents.
        /// </summary>
        [JsonProperty("items")]
        public IReadOnlyList<Item> Contents { get; set; }
    }
}
