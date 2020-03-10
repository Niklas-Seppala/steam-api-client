using System;
using System.Collections.Generic;

namespace SteamApi.Models.CsGo
{
    /// <summary>
    /// CS GO server status model
    /// </summary>
    [Serializable]
    public sealed class CsGoServerStatus
    {
        /// <summary>
        /// Server App
        /// </summary>
        public CsGoServerApp App { get; set; }

        /// <summary>
        /// Services
        /// </summary>
        public CsGoServerServices Services { get; set; }

        /// <summary>
        /// Collection of datacenters
        /// </summary>
        public IReadOnlyDictionary<string, CsGoDataCenter> DataCenters { get; set; }

        /// <summary>
        /// Info about Valve's chinese parner Perfect World
        /// </summary>
        public CsGoPerfectWorld PerfectWorld { get; set; }

    }
}