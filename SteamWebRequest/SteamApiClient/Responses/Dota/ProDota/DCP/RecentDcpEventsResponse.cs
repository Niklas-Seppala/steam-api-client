using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response for recent DCP events request.
    /// </summary>
    [Serializable]
    public sealed class RecentDcpEventsResponse : ApiResponse
    {
        /// <summary>
        /// API response contents.
        /// </summary>
        public RecentDcpEvents Contents { get; set; }
    }
}
