using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response for dota 2 team info request.
    /// </summary>
    [Serializable]
    public sealed class DotaTeamInfosResponse : ApiResponse
    {
        /// <summary>
        /// API response status.
        /// </summary>
        public uint Status { get; set; }

        /// <summary>
        /// API response contents.
        /// </summary>
        [JsonProperty("teams")]
        public IReadOnlyList<DotaTeamInfo> Contents { get; set; }
    }
}
