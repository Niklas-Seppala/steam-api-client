using Newtonsoft.Json;
using System;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Hero companion model (Lone Druid's Spirit Bear)
    /// </summary>
    [Serializable]
    public sealed class HeroCompanion
    {
        /// <summary>
        /// Unit name
        /// </summary>
        public string UnitName { get; set; }

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
    }
}
