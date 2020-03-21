using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response to player profile request.
    /// </summary>
    [Serializable]
    public sealed class DotaPlayerProfileResponse : ApiResponse
    {
        /// <summary>
        /// API response contents
        /// </summary>
        public DotaPlayerProfile Contents { get; set; }
    }
}
