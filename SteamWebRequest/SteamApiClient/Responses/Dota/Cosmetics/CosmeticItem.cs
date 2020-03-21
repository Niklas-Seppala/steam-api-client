using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{

    /// <summary>
    /// Dota 2 cosmetic item model class
    /// </summary>
    [Serializable]
    public sealed class CosmeticItem
    {
        /// <summary>
        /// Item's original id. Id changes when item
        /// is traded.
        /// </summary>
        [JsonProperty("original_id")]
        public ulong OriginalId { get; set; }

        /// <summary>
        /// Item's current id
        /// </summary>
        public ulong Id { get; set; }

        /// <summary>
        /// Item index
        /// </summary>
        public ulong DefIndex { get; set; }

        /// <summary>
        /// Item level
        /// </summary>
        public uint Level { get; set; }

        /// <summary>
        /// Item quality
        /// </summary>
        public uint Quality { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ulong Inventory { get; set; }

        /// <summary>
        /// Item quantity
        /// </summary>
        public uint Quantity { get; set; }
    }
}
