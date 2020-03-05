using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Dota 2 tournament model
    /// </summary>
    public class Tournament
    {
        /// <summary>
        /// Tournament Id
        /// </summary>
        public ulong Id { get; set; }

        /// <summary>
        /// Tournament Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of tournament matche
        /// </summary>
        public IReadOnlyList<TournamentMatch> Matches { get; set; }
    }
}
