using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class RealtimeMatchStats
    {
        public RealTimeMatch Match { get; set; }
        public IReadOnlyList<RealTimeMatchBuilding> Buildings { get; set; }
        [JsonProperty("graph_data")]
        public RealTimeMatchGrapData GraphData { get; set; }
        [JsonProperty("delta_frame")]
        public bool DeltaFrame { get; set; }
        public IReadOnlyList<RealTimeMatchTeam> Teams { get; set; }
    }
}
