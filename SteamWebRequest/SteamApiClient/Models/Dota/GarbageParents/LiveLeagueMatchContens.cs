using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    internal class LiveLeagueMatchContens
    {
        public uint Status { get; set; }
        public IReadOnlyCollection<LiveLeagueMatch> Games { get; set; }
    }
}
