using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Realtime dota 2 match stats
    /// </summary>
    [Serializable]
    public sealed class RealtimeMatchStats
    {
        /// <summary>
        /// Realtime dota2 match
        /// </summary>
        public RealTimeMatch Match { get; set; }

        /// <summary>
        /// List of the buildings in realtime dota 2 match
        /// </summary>
        public IReadOnlyList<RealTimeMatchBuilding> Buildings { get; set; }

        /// <summary>
        /// Realtime dota 2 match graph data
        /// </summary>
        [JsonProperty("graph_data")]
        public RealTimeMatchGrapData GraphData { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("delta_frame")]
        public bool DeltaFrame { get; set; }

        /// <summary>
        /// List of teams in realtime dota2 match
        /// </summary>
        public IReadOnlyList<RealTimeMatchTeam> Teams { get; set; }
    }
}
