using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response for The International dota 2 tournament
    /// prize pool request.
    /// </summary>
    [Serializable]
    public sealed class InternationalPrizePoolResponse : ApiResponse
    {
        /// <summary>
        /// API response contents (dollars).
        /// </summary>
        [JsonProperty("dollars")]
        public uint Contents { get; set; }
    }
}
