using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Dota 2 player model
    /// </summary>
    [Serializable]
    public sealed class Player
    {
        [JsonProperty("player_slot")]
        [JsonConverter(typeof(PlayerSlotConverter))]
        public PlayerSlot PlayerSlot { get; set; }

        /// <summary>
        /// Item at item slot 0
        /// </summary>
        public uint Item_0 { get; set; }

        /// <summary>
        /// Item at item slot 1
        /// </summary>
        public uint Item_1 { get; set; }

        /// <summary>
        /// Item at item slot 2
        /// </summary>
        public uint Item_2 { get; set; }

        /// <summary>
        /// Item at item slot 3
        /// </summary>
        public uint Item_3 { get; set; }

        /// <summary>
        /// Item at item slot 4
        /// </summary>
        public uint Item_4 { get; set; }

        /// <summary>
        /// Item at item slot 5
        /// </summary>
        public uint Item_5 { get; set; }

        /// <summary>
        /// Item at backpack slot 0
        /// </summary>
        public uint Backpack_0 { get; set; }

        /// <summary>
        /// Item at backpack slot 1
        /// </summary>
        public uint Backpack_1 { get; set; }

        /// <summary>
        /// Item at backpack slot 2
        /// </summary>
        public uint Backpack_2 { get; set; }

        /// <summary>
        /// Item at neutral item slot
        /// </summary>
        [JsonProperty("item_neutral")]
        public uint NeutralItem { get; set; }

        /// <summary>
        /// Kills at the end of the match
        /// </summary>
        public uint Kills { get; set; }

        /// <summary>
        /// Assists at the end of the match
        /// </summary>
        public uint Assists { get; set; }

        /// <summary>
        /// Deaths at the end of the match
        /// </summary>
        public uint Deaths { get; set; }

        /// <summary>
        /// KDA at the end of the match
        /// (kill-death-assist ratio) kills+assist/deaths
        /// </summary>
        public double KDA => (Kills + Assists) / Deaths;

        /// <summary>
        /// Player 32-bit steam id
        /// </summary>
        [JsonProperty("account_id")]
        public uint Id32 { get; set; }

        /// <summary>
        /// Player 64-bit steam id
        /// </summary>
        public ulong Id64 => SteamIdConverter.SteamIdTo64(Id32);

        /// <summary>
        /// Id of the played hero
        /// </summary>
        [JsonProperty("hero_id")]
        public uint HeroId { get; set; }

        /// <summary>
        /// Leaver status
        /// </summary>
        [JsonProperty("Leaver_status")]
        public uint Leaver_status { get; set; }

        /// <summary>
        /// Total lasthits at the end of the match
        /// </summary>
        [JsonProperty("last_hits")]
        public uint LastHits { get; set; }

        /// <summary>
        /// Total denies at the end of the match
        /// </summary>
        public uint Denies { get; set; }

        /// <summary>
        /// Calculated XPM at the end of the match
        /// </summary>
        [JsonProperty("Xp_per_min")]
        public uint XPM { get; set; }

        /// <summary>
        /// Level at the end of the match
        /// </summary>
        public uint Level { get; set; }

        /// <summary>
        /// Damage done to heroes at the end of the match
        /// </summary>
        [JsonProperty("hero_damage")]
        public uint Damage { get; set; }

        /// <summary>
        /// (Damage done to heroes) - (damage reduction) at the end
        /// of the match
        /// </summary>
        [JsonProperty("scaled_hero_damage")]
        public uint ScaledDamage { get; set; }

        /// <summary>
        /// Scaled building damage at the end of the match
        /// </summary>
        [JsonProperty("scaled_tower_damage")]
        public uint ScaledBuildingDamage { get; set; }

        /// <summary>
        /// Total hero healing at the end of the match
        /// </summary>
        [JsonProperty("hero_healing")]
        public uint Healing { get; set; }

        /// <summary>
        /// Scaled healing at the end of the match
        /// </summary>
        [JsonProperty("scaled_hero_healing")]
        public uint ScaledHealing { get; set; }

        /// <summary>
        /// GPM at the end of the match (gold/min)
        /// </summary>
        [JsonProperty("gold_per_min")]
        public uint GPM { get; set; }

        /// <summary>
        /// Total gold player has spent at the end of
        /// the match
        /// </summary>
        [JsonProperty("gold_spent")]
        public uint GoldSpent { get; set; }

        /// <summary>
        /// Total amount of gold in player pocket at the
        /// end of the match
        /// </summary>
        public uint GoldInPocket { get; set; }

        /// <summary>
        /// Total amount gold gained in the match
        /// </summary>
        public uint TotalGoldGained => GoldInPocket + GoldSpent;

        /// <summary>
        /// Player's name
        /// </summary>
        [JsonProperty("persona")]
        public string PersonaName { get; set; }

        /// <summary>
        /// List of ability upgrade events during the match
        /// </summary>
        [JsonProperty("ability_upgrades")]
        public IReadOnlyList<AbilityUpgradeEvent> AbilityUpgrades { get; set; }

        /// <summary>
        /// List of the additional player's units (Lone Druid's Spirit Bear)
        /// </summary>
        [JsonProperty("additional_units")]
        public IReadOnlyList<HeroCompanion> Companions { get; set; }
    }
}
