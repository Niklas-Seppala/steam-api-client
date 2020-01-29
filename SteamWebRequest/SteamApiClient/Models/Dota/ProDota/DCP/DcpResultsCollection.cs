using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    public class DcpResultsCollection
    {
        [JsonProperty("results")]
        public IReadOnlyCollection<DcpResults> Content { get; set; }
    }
}
