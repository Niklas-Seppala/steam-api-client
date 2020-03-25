using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response to league listing request.
    /// </summary>
    [Serializable]
    public sealed class LeagueListingResponse : ApiResponse
    {
        /// <summary>
        /// API response contents.
        /// </summary>
        [JsonProperty("leagues")]
        public IReadOnlyList<League> Contents { get; set; }
    }
}
