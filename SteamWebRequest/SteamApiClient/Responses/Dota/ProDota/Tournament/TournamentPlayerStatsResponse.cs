using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response for tournament player stats request.
    /// </summary>
    [Serializable]
    public sealed class TournamentPlayerStatsResponse : ApiResponse
    {
        /// <summary>
        /// Response status info.
        /// </summary>
        public string StatusDetail { get; set; }

        /// <summary>
        /// Response status.
        /// </summary>
        public uint Status { get; set; }

        /// <summary>
        /// API response contents.
        /// </summary>
        [JsonProperty("result")]
        public TournamentPlayerStats Contents { get; set; }
    }
}
