using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    internal class LiveLeagueMatchContens
    {
        public ushort Status { get; set; }
        public IReadOnlyCollection<LiveLeagueMatch> Games { get; set; }
    }
}
