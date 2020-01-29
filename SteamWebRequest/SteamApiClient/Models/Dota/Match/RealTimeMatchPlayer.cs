using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    public class RealTimeMatchPlayer
    {
        public uint AccountId { get; set; }
        public uint PlayerId { get; set; }
        public string Name { get; set; }
        public byte Team { get; set; }
        public ushort HeroId { get; set; }
        public byte Level { get; set; }

        [JsonProperty("kill_count")]
        public byte Kills { get; set; }

        [JsonProperty("assists_count")]
        public byte Assists { get; set; }

        [JsonProperty("death_count")]
        public byte Deaths { get; set; }

        [JsonProperty("denies_count")]
        public ushort Denies { get; set; }

        [JsonProperty("lh_count")]
        public ushort LastHits { get; set; }

        public ushort Gold { get; set; }

        public double X { get; set; }
        public double Y { get; set; }

        [JsonProperty("net_worth")]
        public ushort NetWorth { get; set; }

        public IReadOnlyCollection<ushort> Abilities { get; set; }
        public IReadOnlyCollection<ushort> Items { get; set; }
    }
}
