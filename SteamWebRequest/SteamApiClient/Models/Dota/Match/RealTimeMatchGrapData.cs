using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class RealTimeMatchGrapData
    {
        [JsonProperty("graph_gold")]
        public IReadOnlyCollection<short> GoldGraph { get; set; }
    }
}
