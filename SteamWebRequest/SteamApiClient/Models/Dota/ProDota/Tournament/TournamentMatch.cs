using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class TournamentMatch
    {
        public ulong NodeId { get; set; }
        public ulong ActualMatchTime { get; set; }
        public ulong MatchTime { get; set; }
        public bool Tied { get; set; }
        public IReadOnlyList<TournamentTeam> Teams { get; set; }
    }
}
