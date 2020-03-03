using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class Tournament
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public IReadOnlyList<TournamentMatch> Matches { get; set; }
    }
}
