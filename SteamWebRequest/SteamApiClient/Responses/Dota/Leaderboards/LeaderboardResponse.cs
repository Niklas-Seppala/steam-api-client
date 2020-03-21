using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response for leaderboard request.
    /// </summary>
    [Serializable]
    public sealed class LeaderboardResponse : ApiResponse
    {
        /// <summary>
        /// API response contents
        /// </summary>
        public Leaderboard Contents { get; set; }
    }
}
