using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Dota 2 Pro tournament match model
    /// </summary>
    [Serializable]
    public sealed class TournamentMatch
    {
        /// <summary>
        /// Tournament series node id
        /// </summary>
        public ulong NodeId { get; set; }

        /// <summary>
        /// Actual match time
        /// </summary>
        public ulong ActualMatchTime { get; set; }

        /// <summary>
        /// Match time
        /// </summary>
        public ulong MatchTime { get; set; }

        /// <summary>
        /// Is series tied
        /// </summary>
        public bool Tied { get; set; }

        /// <summary>
        /// List of dota 2 teams in the series
        /// </summary>
        public IReadOnlyList<TournamentTeam> Teams { get; set; }
    }
}
