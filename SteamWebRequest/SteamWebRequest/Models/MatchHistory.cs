﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace SteamWebRequest.Models
{
    public sealed class MatchHistory
    {
        [JsonProperty("Result")]
        public MatchHistoryContent Content { get; set; }
    }

    public sealed class MatchHistoryContent
    {
        [JsonProperty("total_results")]
        public ushort TotalCount { get; set; }

        [JsonProperty("results_remaining")]
        public ushort Remaining { get; set; }

        public byte Status { get; set; }
        public string StatusDetail { get; set; }
        public List<Match> Matches { get; set; }
        public int Count { get => this.Matches.Count; }
    }
}
