using Newtonsoft.Json;
using System.Collections.Specialized;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Player's stats in tournament game
    /// </summary>
    public class TournamentPlayerStatsMatch
    {
        private BitVector32 _player_slot;

        /// <summary>
        /// PLayer slot as bitvector. Don't use this.
        /// Details can be extracted from bitvector using IsDire/IsRadiant
        /// and TeamPosition properties
        /// </summary>
        [JsonConverter(typeof(PlayerSlotConverter))]
        [JsonProperty("player_slot")]
        public BitVector32 PlayerSlot { set => _player_slot = value; }

        /// <summary>
        /// Is player Dire
        /// </summary>
        public bool IsDire => _player_slot[128];

        /// <summary>
        /// Is player Radiant
        /// </summary>
        public bool IsRadiant => !IsDire;

        /// <summary>
        /// Players team position
        /// </summary>
        public uint TeamPosition => (uint)(_player_slot[BitVector32.CreateSection(4)] + 1);

        /// <summary>
        /// Id of the hero being played
        /// </summary>
        [JsonProperty("hero_id")]
        public uint HeroId { get; set; }

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
        /// Item at backpack slot 3
        /// </summary>
        public uint Backpack_3 { get; set; }

        /// <summary>
        /// Item at neutral slot
        /// </summary>
        [JsonProperty("item_neutral")]
        public ushort NeutralItem { get; set; }

        /// <summary>
        /// Player's total kills during the match
        /// </summary>
        public uint Kills { get; set; }

        /// <summary>
        /// Player's total assists during the match
        /// </summary>
        public uint Assists { get; set; }

        /// <summary>
        /// Player's total deaths during the match
        /// </summary>
        public uint Deaths { get; set; }

        /// <summary>
        /// Player's calculated "Kill-Death-Assist" ratio.
        /// (kills + assists / deaths)
        /// </summary>
        public double KDA => (Kills + Assists) / Deaths;

        /// <summary>
        /// Player's total lasthists during the match
        /// </summary>
        [JsonProperty("last_hits")]
        public uint LastHits { get; set; }

        /// <summary>
        /// Player's total denies during the match
        /// </summary>
        public uint Denies { get; set; }

        /// <summary>
        /// Players calculated GPM after the match
        /// (gold/min)
        /// </summary>
        [JsonProperty("gold_per_min")]
        public uint GPM { get; set; }

        /// <summary>
        /// Player's calculated XPM after the match
        /// (xp/min)
        /// </summary>
        [JsonProperty("Xp_per_min")]
        public uint XPM { get; set; }

        /// <summary>
        /// Player's level at the end of the match
        /// </summary>
        public uint Level { get; set; }

        /// <summary>
        /// Player's total networth at the end of the match
        /// </summary>
        public uint NetWorth { get; set; }

        /// <summary>
        /// Did player win?
        /// </summary>
        public bool Win { get; set; }

        /// <summary>
        /// Game duration
        /// </summary>
        public uint Duration { get; set; }

        /// <summary>
        /// Match Id
        /// </summary>
        [JsonProperty("match_id")]
        public string MatchId { get; set; }
    }
}
