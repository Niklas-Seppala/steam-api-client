using System;
using System.Collections.Specialized;

namespace SteamApi
{
    /// <summary>
    /// Dota 2 barracks status data. True means that building is
    /// still standing.
    /// </summary>
    [Serializable]
    public readonly struct BarracksStatus
    {
        /// <summary>
        /// Bottom ranged barracks
        /// </summary>
        public bool BottomRanged { get; }

        /// <summary>
        /// Bottom melee barracks
        /// </summary>
        public bool BottomMelee { get; }

        /// <summary>
        /// Middle ranged barracks
        /// </summary>
        public bool MiddleRanged { get; }

        /// <summary>
        /// Middle melee barracks
        /// </summary>
        public bool MiddleMelee { get; }

        /// <summary>
        /// Top ranged barracks
        /// </summary>
        public bool TopRanged { get; }

        /// <summary>
        /// Top melee barracks
        /// </summary>
        public bool TopMelee { get; }

        /// <summary>
        /// Instantiates BarracksStatus object from byte.
        /// </summary>
        /// <param name="value">Barracks status value compressed to 32-bit integer</param>
        // ┌─┬───────────── not used.
        // │ │ ┌─────────── bottom Ranged
        // │ │ │ ┌───────── bottom Melee
        // │ │ │ │ ┌─────── middle Ranged
        // │ │ │ │ │ ┌───── middle Melee
        // │ │ │ │ │ │ ┌─── top Ranged
        // │ │ │ │ │ │ │ ┌─ top Melee
        // 0 0 0 0 0 0 0 0
        public BarracksStatus(int value)
        {
            BitVector32 bits = new BitVector32(value);

            BottomRanged = bits[32];
            BottomMelee = bits[16];
            MiddleRanged = bits[8];
            MiddleMelee = bits[4];
            TopRanged = bits[2];
            TopMelee = bits[1];
        }
    }
}
