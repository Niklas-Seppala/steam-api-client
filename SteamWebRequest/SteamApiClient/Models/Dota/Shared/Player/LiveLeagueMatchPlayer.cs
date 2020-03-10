using Newtonsoft.Json;
using System;
using System.Collections.Specialized;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Live league match player model
    /// </summary>
    [Serializable]
    public sealed class LiveLeagueMatchPlayer
    {
        /// <summary>
        /// Player's data about how they fit in their team.
        /// </summary>
        [JsonProperty("player_slot")]
        [JsonConverter(typeof(PlayerSlotConverter))]
        public PlayerSlot PlayerSlot { get; set; }

        /// <summary>
        /// PLayer's X-coordinate on minimap
        /// </summary>
        public double Position_X { get; set; }

        /// <summary>
        /// PLayer's Y-coordinate on minimap
        /// </summary>
        public double Position_Y { get; set; }

        /// <summary>
        /// Current networth
        /// </summary>
        [JsonProperty("net_worth")]
        public uint NetWorth { get; set; }

        /// <summary>
        /// Player's respawn timer (if dead)
        /// </summary>
        [JsonProperty("respawn_timer")]
        public uint RespawnTimer { get; set; }

        /// <summary>
        /// Item at itemslot 0
        /// </summary>
        public uint Item0 { get; set; }

        /// <summary>
        /// Item at itemslot 1
        /// </summary>
        public uint Item1 { get; set; }

        /// <summary>
        /// Item at itemslot 2
        /// </summary>
        public uint Item2 { get; set; }

        /// <summary>
        /// Item at itemslot 3
        /// </summary>
        public uint Item3 { get; set; }

        /// <summary>
        /// Item at itemslot 4
        /// </summary>
        public uint Item4 { get; set; }

        /// <summary>
        /// Item at itemslot 5
        /// </summary>
        public uint Item5 { get; set; }

        /// <summary>
        /// Current cooldown of player's ultimate ability
        /// </summary>
        [JsonProperty("ultimate_cooldown")]
        public uint UltimateCooldown { get; set; }

        /// <summary>
        /// Player's ultimate ability state
        /// </summary>
        [JsonProperty("ultimate_state")]
        public uint UltimateState { get; set; }

        /// <summary>
        /// Current XPM  
        /// (xp/min)
        /// </summary>
        [JsonProperty("xp_per_min")]
        public ushort XPM { get; set; }

        /// <summary>
        /// Current GPM 
        /// (gold/min)
        /// </summary>
        [JsonProperty("gold_per_min")]
        public ushort GMP { get; set; }

        /// <summary>
        /// Current level
        /// </summary>
        public uint Level { get; set; }

        /// <summary>
        /// Current amount of gold
        /// </summary>
        public ushort Gold { get; set; }

        /// <summary>
        /// Current deny count
        /// </summary>
        public ushort Denies { get; set; }

        /// <summary>
        /// Current lasthit count
        /// </summary>
        [JsonProperty("last_hits")]
        public ushort LastHits { get; set; }

        /// <summary>
        /// Current assist count
        /// </summary>
        public uint Assists { get; set; }

        /// <summary>
        /// Current death count
        /// </summary>
        [JsonProperty("death")]
        public uint Deaths { get; set; }

        /// <summary>
        /// Current kill count
        /// </summary>
        public uint Kills { get; set; }

        /// <summary>
        /// Played hero id
        /// </summary>
        [JsonProperty("hero_id")]
        public ushort HeroId { get; set; }

        /// <summary>
        /// Account id32
        /// </summary>
        [JsonProperty("account_id")]
        public uint AccountId { get; set; }
    }
}
