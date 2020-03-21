using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response to unique users request.
    /// </summary>
    [Serializable]
    public class UniqueUsersResponse : ApiResponse
    {
        /// <summary>
        /// Unique users response contents
        /// </summary>
        public IReadOnlyDictionary<string, uint> Contents { get; set; }
    }
}
