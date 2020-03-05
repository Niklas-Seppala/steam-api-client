using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Realtime dota 2 match graph model
    /// </summary>
    public class RealTimeMatchGrapData
    {
        /// <summary>
        /// Gold graph
        /// </summary>
        [JsonProperty("graph_gold")]
        public IReadOnlyList<int> GoldGraph { get; set; }
    }
}
