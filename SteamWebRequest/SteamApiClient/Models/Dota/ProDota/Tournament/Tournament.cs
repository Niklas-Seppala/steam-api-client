using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class Tournament
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public IReadOnlyCollection<TournamentMatch> Matches { get; set; }
    }
}
