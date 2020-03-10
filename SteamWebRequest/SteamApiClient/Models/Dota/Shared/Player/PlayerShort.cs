using Newtonsoft.Json;
using System.Collections.Specialized;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Light version of PLayer model
    /// </summary>
    public sealed class PlayerShort
    {
        //private BitVector32 _player_slot;

        ///// <summary>
        ///// Player slot bitvector. Dont touch this, get values from IsDire/IsRadiant
        ///// and TeamPosition properties
        ///// </summary>
        //[JsonConverter(typeof(PlayerSlotConverter))]
        //[JsonProperty("player_slot")]
        //public BitVector32 PlayerSlot { set => _player_slot = value; }

        /// <summary>
        /// Player's data about how they fit in their team.
        /// </summary>
        [JsonConverter(typeof(PlayerSlotConverter))]
        [JsonProperty("player_slot")]
        public PlayerSlot PlayerSlot { get; set; }

        ///// <summary>
        ///// Is player on Dire team
        ///// </summary>
        //public bool IsDire => _player_slot[128];

        ///// <summary>
        ///// Is player on Radiant team
        ///// </summary>
        //public bool IsRadiant => !IsDire;

        ///// <summary>
        ///// Player's position on the team
        ///// </summary>
        //public uint TeamPosition => (uint)(_player_slot[BitVector32.CreateSection(4)] + 1);

        /// <summary>
        /// 32-bit steam account id
        /// </summary>
        [JsonProperty("account_id")]
        public uint Id32 { get; set; }

        /// <summary>
        /// Hero id
        /// </summary>
        [JsonProperty("hero_id")]
        public uint HeroId { get; set; }
    }
}
