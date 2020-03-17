using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Realtime dota 2 match player model
    /// </summary>
    [Serializable]
    public sealed class RealTimeMatchPlayer
    {
        /// <summary>
        /// 32-bit steam account id
        /// </summary>
        public uint AccountId { get; set; }

        /// <summary>
        /// Player id
        /// </summary>
        public uint PlayerId { get; set; }

        /// <summary>
        /// Player name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// team id
        /// </summary>
        public uint Team { get; set; }

        /// <summary>
        /// Hero id
        /// </summary>
        public uint HeroId { get; set; }

        /// <summary>
        /// Current level
        /// </summary>
        public uint Level { get; set; }

        /// <summary>
        /// Current kill count
        /// </summary>
        [JsonProperty("kill_count")]
        public uint Kills { get; set; }

        /// <summary>
        /// Current assist count
        /// </summary>
        [JsonProperty("assists_count")]
        public uint Assists { get; set; }

        /// <summary>
        /// Current death count
        /// </summary>
        [JsonProperty("death_count")]
        public uint Deaths { get; set; }

        /// <summary>
        /// Current denies count
        /// </summary>
        [JsonProperty("denies_count")]
        public uint Denies { get; set; }

        /// <summary>
        /// Current lasthit count
        /// </summary>
        [JsonProperty("lh_count")]
        public uint LastHits { get; set; }

        /// <summary>
        /// Current gold count
        /// </summary>
        public uint Gold { get; set; }

        /// <summary>
        /// X-coordinate on minimap
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Y-coordinate on minimap
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Current networth
        /// </summary>
        [JsonProperty("net_worth")]
        public uint NetWorth { get; set; }

        /// <summary>
        /// List of abilities player has leveled
        /// </summary>
        public IReadOnlyList<uint> Abilities { get; set; }

        /// <summary>
        /// List of items player has obtained
        /// </summary>
        public IReadOnlyList<uint> Items { get; set; }
    }
}
