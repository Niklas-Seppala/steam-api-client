using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Dota 2 player's equiped item model
    /// </summary>
    [Serializable]
    public sealed class EquipedItem
    {
        /// <summary>
        /// Item index
        /// </summary>
        public ulong DefIndex { get; set; }

        /// <summary>
        /// item style
        /// </summary>
        public uint Style { get; set; }
    }
}
