using System.Collections.Generic;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace SteamWebRequest.Models
{
    public sealed class Player
    {
        private BitVector32 _player_slot;
        [JsonConverter(typeof(PlayerSlotConverter))]
        public BitVector32 Player_slot { set => _player_slot = value; }
        public bool IsDire { get => _player_slot[128]; }
        public bool IsRadiant { get => !this.IsDire; }
        public byte TeamPosition { get => (byte)(_player_slot[BitVector32.CreateSection(4)] + 1); }

        [JsonProperty("account_id")]
        public ulong Id { get; set; }

        [JsonProperty("hero_id")]
        public ushort HeroId { get; set; }

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

        public byte Kills { get; set; }
        public byte Assists { get; set; }
        public byte Deaths { get; set; }
        public float KDA { get => (this.Kills + this.Assists) / this.Deaths; }

        [JsonProperty("Leaver_status")]
        public byte Leaver_status { get; set; }

        [JsonProperty("last_hits")]
        public ushort LastHits { get; set; }
        public ushort Denies { get; set; }

        [JsonProperty("Xp_per_min")]
        public ushort XPM { get; set; }
        public byte LevelAtEnd { get; set; }

        [JsonProperty("hero_damage")]
        public uint Damage { get; set; }
        [JsonProperty("scaled_hero_damage")]
        public uint ScaledDamage { get; set; }

        [JsonProperty("scaled_tower_damage")]
        public uint ScaledBuildingDamage { get; set; }

        [JsonProperty("hero_healing")]
        public uint Healing { get; set; }
        [JsonProperty("scaled_hero_healing")]
        public uint ScaledHealing { get; set; }

        [JsonProperty("gold_per_min")]
        public ushort GPM { get; set; }

        [JsonProperty("gold_spent")]
        public uint GoldSpent { get; set; }
        public uint GoldInPocket { get; set; }
        public uint TotalGoldGained { get => this.GoldInPocket + this.GoldSpent; }

        [JsonProperty("ability_upgrades")]
        public List<AbilityUpgradeEvent> AbilityUpgrades { get; set; }
    }

}
