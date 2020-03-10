using System;

namespace SteamApi.Models.Dota
{
    [Serializable]
    public sealed class EquipedItem
    {
        public ulong DefIndex { get; set; }
        public uint Style { get; set; }
    }
}
