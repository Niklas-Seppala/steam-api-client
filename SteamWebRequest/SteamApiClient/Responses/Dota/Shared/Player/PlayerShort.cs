using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Light version of PLayer model
    /// </summary>
    [Serializable]
    public sealed class PlayerShort
    {
        /// <summary>
        /// Player's data about how they fit in their team.
        /// </summary>
        [JsonConverter(typeof(PlayerSlotConverter))]
        [JsonProperty("player_slot")]
        public PlayerSlot PlayerSlot { get; set; }

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
