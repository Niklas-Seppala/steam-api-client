using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    internal class LiveLeagueMatchContens
    {
        public uint Status { get; set; }
        public IReadOnlyList<LiveLeagueMatch> Games { get; set; }
    }
}
