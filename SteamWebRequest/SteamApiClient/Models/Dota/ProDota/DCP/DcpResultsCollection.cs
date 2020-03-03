using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class DcpResultsCollection
    {
        [JsonProperty("results")]
        public IReadOnlyList<DcpResults> Content { get; set; }
    }
}
