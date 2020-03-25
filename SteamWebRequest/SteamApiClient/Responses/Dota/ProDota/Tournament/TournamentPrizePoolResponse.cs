using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response to tournament prize pool request.
    /// </summary>
    [Serializable]
    public sealed class TournamentPrizePoolResponse : ApiResponse
    {
        /// <summary>
        /// API response contents.
        /// </summary>
        [JsonProperty("result")]
        public TournamentPrizePool Contents { get; set; }

        /// <summary>
        /// Response status.
        /// </summary>
        public uint Status { get => Contents != null ? Contents.Status : 0; }
    }
}
