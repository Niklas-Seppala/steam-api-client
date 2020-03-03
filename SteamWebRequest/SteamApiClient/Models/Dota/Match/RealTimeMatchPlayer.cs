using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class RealTimeMatchPlayer
    {
        public uint AccountId { get; set; }
        public uint PlayerId { get; set; }
        public string Name { get; set; }
        public uint Team { get; set; }
        public uint HeroId { get; set; }
        public uint Level { get; set; }
        [JsonProperty("kill_count")]
        public uint Kills { get; set; }
        [JsonProperty("assists_count")]
        public uint Assists { get; set; }
        [JsonProperty("death_count")]
        public uint Deaths { get; set; }
        [JsonProperty("denies_count")]
        public uint Denies { get; set; }
        [JsonProperty("lh_count")]
        public uint LastHits { get; set; }
        public uint Gold { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        [JsonProperty("net_worth")]
        public uint NetWorth { get; set; }
        public IReadOnlyList<ushort> Abilities { get; set; }
        public IReadOnlyList<ushort> Items { get; set; }
    }
}
