using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace SteamApi.Models.Dota
{
    public class Player
    {
        private BitVector32 _player_slot;
        [JsonConverter(typeof(PlayerSlotConverter))]
        [JsonProperty("player_slot")]
        public BitVector32 PlayerSlot { set => _player_slot = value; }
        public bool IsDire => _player_slot[128];
        public bool IsRadiant => !IsDire;
        public uint TeamPosition => (uint)(_player_slot[BitVector32.CreateSection(4)] + 1);

        public uint Item_0 { get; set; }
        public uint Item_1 { get; set; }
        public uint Item_2 { get; set; }
        public uint Item_3 { get; set; }
        public uint Item_4 { get; set; }
        public uint Item_5 { get; set; }
        public uint Backpack_0 { get; set; }
        public uint Backpack_1 { get; set; }
        public uint Backpack_2 { get; set; }

        [JsonProperty("item_neutral")]
        public uint NeutralItem { get; set; }

        public uint Kills { get; set; }
        public uint Assists { get; set; }
        public uint Deaths { get; set; }
        public double KDA => (Kills + Assists) / Deaths;

        [JsonProperty("account_id")]
        public uint Id32 { get; set; }
        public ulong Id64 => SteamIdConverter.SteamIdTo64(Id32);

        [JsonProperty("hero_id")]
        public uint HeroId { get; set; }

        [JsonProperty("Leaver_status")]
        public uint Leaver_status { get; set; }

        [JsonProperty("last_hits")]
        public uint LastHits { get; set; }

        public uint Denies { get; set; }

        [JsonProperty("Xp_per_min")]
        public uint XPM { get; set; }

        public uint Level { get; set; }

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
        public uint GPM { get; set; }

        [JsonProperty("gold_spent")]
        public uint GoldSpent { get; set; }

        public uint GoldInPocket { get; set; }
        public uint TotalGoldGained => GoldInPocket + GoldSpent;

        [JsonProperty("persona")]
        public string PersonaName { get; set; }

        [JsonProperty("ability_upgrades")]
        public IReadOnlyCollection<AbilityUpgradeEvent> AbilityUpgrades { get; set; }

        [JsonProperty("additional_units")]
        public IReadOnlyCollection<HeroCompanion> Companions { get; set; }
    }
}
