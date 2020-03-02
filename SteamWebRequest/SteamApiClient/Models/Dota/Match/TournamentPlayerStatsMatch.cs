using Newtonsoft.Json;
using System;
using System.Collections.Specialized;

namespace SteamApi.Models.Dota
{
    public class TournamentPlayerStatsMatch
    {
        private BitVector32 _player_slot;
        [JsonConverter(typeof(PlayerSlotConverter))]
        [JsonProperty("player_slot")]
        public BitVector32 PlayerSlot { set => _player_slot = value; }
        public bool IsDire => _player_slot[128];
        public bool IsRadiant => !IsDire;
        public uint TeamPosition => (uint)(_player_slot[BitVector32.CreateSection(4)] + 1);

        [JsonProperty("hero_id")]
        public uint HeroId { get; set; }
        public uint Item_0 { get; set; }
        public uint Item_1 { get; set; }
        public uint Item_2 { get; set; }
        public uint Item_3 { get; set; }
        public uint Item_4 { get; set; }
        public uint Item_5 { get; set; }
        public uint Backpack_0 { get; set; }
        public uint Backpack_1 { get; set; }
        public uint Backpack_2 { get; set; }
        public uint Backpack_3 { get; set; }

        [JsonProperty("item_neutral")]
        public ushort NeutralItem { get; set; }

        public uint Kills { get; set; }
        public uint Assists { get; set; }
        public uint Deaths { get; set; }
        public double KDA => (this.Kills + this.Assists) / this.Deaths;

        [JsonProperty("last_hits")]
        public uint LastHits { get; set; }

        public uint Denies { get; set; }

        [JsonProperty("gold_per_min")]
        public uint GPM { get; set; }

        [JsonProperty("Xp_per_min")]
        public uint XPM { get; set; }

        public uint Level { get; set; }

        public uint NetWorth { get; set; }

        public bool Win { get; set; }

        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan Duration { get; set; }

        [JsonProperty("match_id")]
        public string MatchId { get; set; }
    }
}
