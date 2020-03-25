using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response to Dota 2 Team request.
    /// </summary>
    [Serializable]
    public sealed class DotaTeamResponse : ApiResponse
    {
        /// <summary>
        /// API response contents.
        /// </summary>
        public DotaTeam Contents { get; set; }
    }
}
