using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    internal class MatchHistoryBySeqContainer
    {
        public uint Status { get; set; }
        public IList<MatchDetails> Matches { get; set; }
    }
}
