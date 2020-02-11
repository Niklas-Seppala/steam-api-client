using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class MatchHistoryResponse
    {
        [JsonProperty("total_results")]
        public ushort TotalCount { get; set; }

        [JsonProperty("results_remaining")]
        public ushort Remaining { get; set; }

        public byte Status { get; set; }
        public string StatusDetail { get; set; }
        public IReadOnlyCollection<MatchShort> Matches { get; set; }
    }
}
