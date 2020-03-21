using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Web response for dota 2 item info request.
    /// </summary>
    [Serializable]
    public sealed class ItemsInfoResponse : ApiResponse
    {
        /// <summary>
        /// API response contents.
        /// </summary>
        [JsonProperty("itemdata")]
        public IReadOnlyDictionary<string, Item> Contents { get; set; }
    }
}
