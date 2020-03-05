using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Collection of pro dota 2 DCP results
    /// </summary>
    public class DcpResultsCollection
    {
        /// <summary>
        /// List of the DCP results
        /// </summary>
        [JsonProperty("results")]
        public IReadOnlyList<DcpResults> DcpResults { get; set; }
    }
}
