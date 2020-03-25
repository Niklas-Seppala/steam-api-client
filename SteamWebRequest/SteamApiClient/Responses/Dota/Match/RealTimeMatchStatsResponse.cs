using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response to real time match stats request.
    /// </summary>
    [Serializable]
    public sealed class RealtimeMatchStatsResponse : ApiResponse
    {
        /// <summary>
        /// API response contents
        /// </summary>
        public RealtimeMatchStats Contents { get; set; }
    }
}
