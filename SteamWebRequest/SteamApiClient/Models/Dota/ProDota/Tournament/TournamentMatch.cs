using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    public class TournamentMatch
    {
        public ulong NodeId { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime ActualMatchTime { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime MatchTime { get; set; }

        public bool Tied { get; set; }

        public IReadOnlyCollection<TournamentTeam> Teams { get; set; }
    }
}
