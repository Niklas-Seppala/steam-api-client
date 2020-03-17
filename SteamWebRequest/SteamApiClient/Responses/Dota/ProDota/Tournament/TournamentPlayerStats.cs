using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Player's stats in the tournament
    /// </summary>
    [Serializable]
    public sealed class TournamentPlayerStats
    {
        /// <summary>
        /// Status
        /// </summary>
        public uint Status { get; set; }

        /// <summary>
        /// Player's account id
        /// </summary>
        [JsonProperty("account_id")]
        public uint AccountId32 { get; set; }

        /// <summary>
        /// Player's nickname
        /// </summary>
        [JsonProperty("persona")]
        public string PersonaName { get; set; }

        /// <summary>
        /// Total Wins so far
        /// </summary>
        public uint Wins { get; set; }

        /// <summary>
        /// Total losses so far
        /// </summary>
        public uint Losses { get; set; }

        /// <summary>
        /// Total kills so far
        /// </summary>
        public ushort Kills { get; set; }

        /// <summary>
        /// Total deaths so far
        /// </summary>
        public ushort Deaths { get; set; }

        /// <summary>
        /// Total assists so far
        /// </summary>
        public ushort Assists { get; set; }

        /// <summary>
        /// Current kill average
        /// </summary>
        [JsonProperty("kills_avarage")]
        public double KillAverage { get; set; }

        /// <summary>
        /// Current death average
        /// </summary>
        [JsonProperty("deaths_average")]
        public double DeathAverage { get; set; }

        /// <summary>
        /// Current assist average
        /// </summary>
        [JsonProperty("assists_average")]
        public double AssistAverage { get; set; }

        /// <summary>
        /// Current GPM average 
        /// (gold/min)
        /// </summary>
        [JsonProperty("gpm_average")]
        public double GpmAverage { get; set; }

        /// <summary>
        /// Current XMP average 
        /// (xp/min)
        /// </summary>
        [JsonProperty("xpm_average")]
        public double XpmAverage { get; set; }

        /// <summary>
        /// Current networth average
        /// </summary>
        [JsonProperty("networth_average")]
        public double NetWorthAverage { get; set; }

        /// <summary>
        /// Current lasthit average
        /// </summary>
        [JsonProperty("last_hits_average")]
        public double LastHitAverage { get; set; }

        /// <summary>
        /// Current herodamage average
        /// </summary>
        [JsonProperty("hero_damage_average")]
        public uint HeroDamageAverage { get; set; }

        /// <summary>
        /// Current building damage average
        /// </summary>
        [JsonProperty("tower_damage_average")]
        public uint TowerDamageAverage { get; set; }

        /// <summary>
        /// Best kill count in a single match
        /// </summary>
        [JsonProperty("best_kills")]
        public uint BestKills { get; set; }

        /// <summary>
        /// Hero id of the best kill count in a single match
        /// </summary>
        [JsonProperty("best_kills_heroid")]
        public uint BestKillsHeroId { get; set; }

        /// <summary>
        /// Best GPM in a single match
        /// </summary>
        [JsonProperty("best_gpm")]
        public uint BestGpm { get; set; }

        /// <summary>
        /// Hero id of the best GPM in a single match
        /// </summary>
        [JsonProperty("best_gpm_heroid")]
        public uint BestGpmHeroId { get; set; }

        /// <summary>
        /// Best XPM in a single match
        /// </summary>
        [JsonProperty("best_networth")]
        public uint BestNetWorth { get; set; }

        /// <summary>
        /// Hero id of the best XPM in a single match
        /// </summary>
        [JsonProperty("best_networth_heroid")]
        public uint BestNetWorthHeroId { get; set; }

        /// <summary>
        /// List of the heroes played
        /// </summary>
        [JsonProperty("heroes_played")]
        public IReadOnlyList<IReadOnlyDictionary<string, ushort>> HeroesPlayed { get; set; }

        /// <summary>
        /// List of the matches played
        /// </summary>
        public IReadOnlyList<TournamentPlayerStatsMatch> Matches { get; set; }
    }
}
