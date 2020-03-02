using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class PlayerItem
    {
        [JsonProperty("original_id")]
        public ulong OriginalId { get; set; }

        public ulong Id { get; set; }
        public ulong DefIndex { get; set; }
        public uint Level { get; set; }
        public uint Quality { get; set; }
        public ulong Inventory { get; set; }
        public uint Quantity { get; set; }
    }

    public class PlayerInventory
    {
        public uint Status { get; set; }

        [JsonProperty("num_backpack_slots")]
        public uint NumBackpackSlots { get; set; }

        public IReadOnlyCollection<PlayerItem> Items { get; set; }
    }
}
