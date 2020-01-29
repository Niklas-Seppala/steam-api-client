using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public class PlayerItem
    {
        [JsonProperty("original_id")]
        public ulong OriginalId { get; set; }

        public ulong Id { get; set; }
        public ulong DefIndex { get; set; }
        public ushort Level { get; set; }
        public byte Quality { get; set; }
        public ulong Inventory { get; set; }
        public ushort Quantity { get; set; }
    }

    public class PlayerInventory
    {
        public byte Status { get; set; }

        [JsonProperty("num_backpack_slots")]
        public ushort NumBackpackSlots { get; set; }

        public IReadOnlyCollection<PlayerItem> Items { get; set; }
    }
}
