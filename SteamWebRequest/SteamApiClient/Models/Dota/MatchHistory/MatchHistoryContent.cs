using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public sealed class MatchHistoryContent
    {
        [JsonProperty("total_results")]
        public ushort TotalCount { get; set; }

        [JsonProperty("results_remaining")]
        public ushort Remaining { get; set; }

        public byte Status { get; set; }
        public string StatusDetail { get; set; }
        public List<Match> Matches { get; set; }
        public int Count => this.Matches.Count;
    }
}
