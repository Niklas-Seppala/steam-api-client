using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Dota 2 player's cosmetic item inventory
    /// </summary>
    [Serializable]
    public sealed class PlayerInventory
    {
        /// <summary>
        /// Inventory status
        /// </summary>
        public uint Status { get; set; }

        /// <summary>
        /// Backpack slots
        /// </summary>
        [JsonProperty("num_backpack_slots")]
        public uint NumBackpackSlots { get; set; }

        /// <summary>
        /// List of dota 2 cosmetic items in player's inventory
        /// </summary>
        public IReadOnlyList<CosmeticItem> Items { get; set; }
    }
}
