using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Realtime dota 2 match graph model
    /// </summary>
    [Serializable]
    public sealed class RealTimeMatchGrapData
    {
        /// <summary>
        /// Gold graph
        /// </summary>
        [JsonProperty("graph_gold")]
        public IReadOnlyList<int> GoldGraph { get; set; }
    }
}
