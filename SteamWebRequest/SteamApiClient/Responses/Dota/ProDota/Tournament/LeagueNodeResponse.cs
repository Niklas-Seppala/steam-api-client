using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response to league node request.
    /// </summary>
    [Serializable]
    public sealed class LeagueNodeResponse : ApiResponse
    {
        /// <summary>
        /// API response contents.
        /// </summary>
        public LeagueNode Contents { get; set; }
    }
}
