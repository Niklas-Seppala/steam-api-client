using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Scoreboard model of the team in live league match
    /// </summary>
    [Serializable]
    public sealed class LiveLeagueMatchScoreboardTeam
    {
        /// <summary>
        /// Team's score
        /// </summary>
        public uint Score { get; set; }

        /// <summary>
        /// team's tower state
        /// </summary>
        [JsonProperty("tower_state")]
        public uint TowerState { get; set; }

        /// <summary>
        /// Team's barracks state
        /// </summary>
        [JsonProperty("barracks_state")]
        public uint BarracksState { get; set; }

        /// <summary>
        /// List of team's picks in drafting phase
        /// </summary>
        public IReadOnlyList<Pick> Picks { get; set; }

        /// <summary>
        /// List of team's bans in drafting phase
        /// </summary>
        public IReadOnlyList<Ban> Bans { get; set; }

        /// <summary>
        /// List of team's players
        /// </summary>
        public IReadOnlyList<LiveLeagueMatchPlayer> Players { get; set; }

        /// <summary>
        /// List of abilities
        /// </summary>
        public IReadOnlyList<IReadOnlyDictionary<string, uint>> Abilities { get; set; }
    }
}
