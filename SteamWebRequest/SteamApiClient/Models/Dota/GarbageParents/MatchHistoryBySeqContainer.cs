using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    internal class MatchHistoryBySeqContainer
    {
        public uint Status { get; set; }
        public IReadOnlyCollection<MatchDetails> Matches { get; set; }
    }
}
