using Newtonsoft.Json;
using System.Collections.Specialized;

namespace SteamApi.Models.Dota
{
    public class LiveLeagueMatchPlayer
    {
        private BitVector32 _player_slot;
        [JsonConverter(typeof(PlayerSlotConverter))]
        public BitVector32 Player_slot { set => _player_slot = value; }
        public bool IsDire => _player_slot[128];
        public bool IsRadiant => !this.IsDire;
        public byte TeamPosition => (byte)(_player_slot[BitVector32.CreateSection(4)] + 1);

        public double Position_X { get; set; }
        public double Position_Y { get; set; }

        [JsonProperty("net_worth")]
        public ushort NetWorth { get; set; }

        [JsonProperty("respawn_timer")]
        public ushort RespawnTimer { get; set; }

        public uint Item0 { get; set; }
        public uint Item1 { get; set; }
        public uint Item2 { get; set; }
        public uint Item3 { get; set; }
        public uint Item4 { get; set; }
        public uint Item5 { get; set; }

        [JsonProperty("ultimate_cooldown")]
        public ushort UltimateCooldown { get; set; }

        [JsonProperty("ultimate_state")]
        public byte UltimateState { get; set; }

        [JsonProperty("xp_per_min")]
        public ushort XPM { get; set; }

        [JsonProperty("gold_per_min")]
        public ushort GMP { get; set; }

        public byte Level { get; set; }

        public ushort Gold { get; set; }

        public ushort Denies { get; set; }

        [JsonProperty("last_hits")]
        public ushort LastHits { get; set; }

        public byte Assists { get; set; }

        [JsonProperty("death")]
        public byte Deaths { get; set; }

        public byte Kills { get; set; }

        [JsonProperty("hero_id")]
        public ushort HeroId { get; set; }

        [JsonProperty("account_id")]
        public uint AccountId { get; set; }
    }
}
