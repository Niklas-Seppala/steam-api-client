using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response for live league match request.
    /// </summary>
    [Serializable]
    public sealed class LiveLeagueMatchesResponse : ApiResponse
    {
        /// <summary>
        /// Status.
        /// </summary>
        public uint Status { get; set; }

        /// <summary>
        /// API response contents.
        /// </summary>
        [JsonProperty("games")]
        public IReadOnlyList<LiveLeagueMatch> Contents { get; set; }
    }
}
