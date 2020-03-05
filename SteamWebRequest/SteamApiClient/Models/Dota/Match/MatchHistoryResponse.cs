using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Match history API response model
    /// </summary>
    public class MatchHistoryResponse
    {
        /// <summary>
        /// Number of total results
        /// </summary>
        [JsonProperty("total_results")]
        public uint TotalCount { get; set; }

        /// <summary>
        /// Results remaining
        /// </summary>
        [JsonProperty("results_remaining")]
        public uint Remaining { get; set; }

        /// <summary>
        /// Response status
        /// </summary>
        public uint Status { get; set; }

        /// <summary>
        /// Response status details
        /// </summary>
        public string StatusDetail { get; set; }

        /// <summary>
        /// List of the requested matches
        /// </summary>
        public IReadOnlyList<MatchShort> Matches { get; set; }
    }
}
