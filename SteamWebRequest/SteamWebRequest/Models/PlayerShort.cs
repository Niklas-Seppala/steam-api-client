using Newtonsoft.Json;
using System.Collections.Specialized;

namespace SteamWebRequest
{
    public sealed class PlayerShort
    {
        private BitVector32 _player_slot;

        [JsonProperty("account_id")]
        public ulong Id { get; set; }

        [JsonProperty("hero_id")]
        public ushort HeroId { get; set; }

        [JsonConverter(typeof(PlayerSlotConverter))]
        [JsonProperty("player_slot")]
        public BitVector32 PlayerSlot { set => _player_slot = value; }

        public bool IsDire => _player_slot[128];
        public bool IsRadiant => !this.IsDire;
        public byte TeamPosition => (byte)(_player_slot[BitVector32.CreateSection(4)] + 1);
    }
}
