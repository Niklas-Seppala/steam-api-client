using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Web API response to hero info request
    /// </summary>
    [Serializable]
    public sealed class HeroInfoResponse : ApiResponse
    {
        /// <summary>
        /// Hero info response contents
        /// </summary>
        public IReadOnlyDictionary<string, HeroInfo> Contents { get; set; }
    }
}
