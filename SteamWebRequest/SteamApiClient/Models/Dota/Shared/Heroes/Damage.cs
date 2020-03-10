using System;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// In Dota 2 actual damage is between min and max
    /// damage amount
    /// </summary>
    [Serializable]
    public sealed class Damage
    {
        /// <summary>
        /// Minimum amount of damage
        /// </summary>
        public uint Min { get; set; }

        /// <summary>
        /// Maximum amount of damage
        /// </summary>
        public uint Max { get; set; }
    }
}
