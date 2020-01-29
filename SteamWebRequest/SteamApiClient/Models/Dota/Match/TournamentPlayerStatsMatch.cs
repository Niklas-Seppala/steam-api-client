using Newtonsoft.Json;
using System;
using System.Collections.Specialized;

namespace SteamApiClient.Models.Dota
{
    public class TournamentPlayerStatsMatch
    {
        private BitVector32 _player_slot;
        [JsonConverter(typeof(PlayerSlotConverter))]
        [JsonProperty("player_slot")]
        public BitVector32 PlayerSlot { set => _player_slot = value; }
        public bool IsDire => _player_slot[128];
        public bool IsRadiant => !this.IsDire;
        public byte TeamPosition => (byte)(_player_slot[BitVector32.CreateSection(4)] + 1);

        public ushort Item_0 { get; set; }
        public ushort Item_1 { get; set; }
        public ushort Item_2 { get; set; }
        public ushort Item_3 { get; set; }
        public ushort Item_4 { get; set; }
        public ushort Item_5 { get; set; }
        public ushort Backpack_0 { get; set; }
        public ushort Backpack_1 { get; set; }
        public ushort Backpack_2 { get; set; }
        public ushort Backpack_3 { get; set; }

        [JsonProperty("item_neutral")]
        public ushort NeutralItem { get; set; }

        public byte Kills { get; set; }
        public byte Assists { get; set; }
        public byte Deaths { get; set; }
        public double KDA => (this.Kills + this.Assists) / this.Deaths;

        [JsonProperty("last_hits")]
        public ushort LastHits { get; set; }

        public ushort Denies { get; set; }

        [JsonProperty("gold_per_min")]
        public ushort GPM { get; set; }

        [JsonProperty("Xp_per_min")]
        public ushort XPM { get; set; }

        public byte Level { get; set; }

        public ushort NetWorth { get; set; }

        public bool Win { get; set; }

        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan Duration { get; set; }

        [JsonProperty("match_id")]
        public string MatchId { get; set; }
    }
}
