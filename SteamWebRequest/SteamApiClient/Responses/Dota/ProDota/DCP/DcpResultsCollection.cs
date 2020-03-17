using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Collection of pro dota 2 DCP results
    /// </summary>
    [Serializable]
    public sealed class DcpResultsCollection
    {
        /// <summary>
        /// List of the DCP results
        /// </summary>
        [JsonProperty("results")]
        public IReadOnlyList<DcpResults> DcpResults { get; set; }
    }
}
