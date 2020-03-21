using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response to schema url request.
    /// </summary>
    [Serializable]
    public sealed class SchemaUrlResponse : ApiResponse
    {
        /// <summary>
        /// Status
        /// </summary>
        public uint Status { get; set; }

        /// <summary>
        /// API response contents.
        /// </summary>
        [JsonProperty("items_game_url")]
        public string Contents { get; set; }
    }
}
