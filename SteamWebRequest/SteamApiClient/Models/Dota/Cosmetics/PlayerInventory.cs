using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    [Serializable]
    public sealed class PlayerInventory
    {
        public uint Status { get; set; }

        [JsonProperty("num_backpack_slots")]
        public uint NumBackpackSlots { get; set; }

        public IReadOnlyList<PlayerItem> Items { get; set; }
    }
}
