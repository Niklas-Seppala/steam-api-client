using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response to item icon path request.
    /// </summary>
    [Serializable]
    public sealed class ItemIconPathResponse : ApiResponse
    {
        /// <summary>
        /// API response content.
        /// </summary>
        [JsonProperty("path")]
        public string Contents { get; set; }
    }
}
