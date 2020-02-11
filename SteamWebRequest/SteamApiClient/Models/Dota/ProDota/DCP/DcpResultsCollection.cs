using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class DcpResultsCollection
    {
        [JsonProperty("results")]
        public IReadOnlyCollection<DcpResults> Content { get; set; }
    }
}
