using System;
using System.Collections.Specialized;

namespace SteamApi
{
    /// <summary>
    /// Dota 2 tower status data. True means that building is
    /// still standing.
    /// </summary>
    [Serializable]
    public readonly struct TowerStatus
    {
        /// <summary>
        /// Ancient top (Tier 4) tower. True means that
        /// it's still standing.
        /// </summary>
        public bool AncientTop { get; }

        /// <summary>
        /// Ancient bottom (Tier 4) tower. True means that
        /// it's still standing.
        /// </summary>
        public bool AncientBottom { get; }

        /// <summary>
        /// Bottom tier 3 tower.
        /// </summary>
        public bool BottomTier_3 { get; }

        /// <summary>
        /// Bottom tier 2 tower.
        /// </summary>
        public bool BottomTier_2 { get; }

        /// <summary>
        /// Bottom tier 1 tower.
        /// </summary>
        public bool BottomTier_1 { get; }

        /// <summary>
        /// Middle tier 3 tower.
        /// </summary>
        public bool MiddleTier_3 { get; }

        /// <summary>
        /// Middle tier 2 tower.
        /// </summary>
        public bool MiddleTier_2 { get; }

        /// <summary>
        /// Middle tier 1 tower.
        /// </summary>
        public bool MiddleTier_1 { get; }

        /// <summary>
        /// Top tier 3 tower.
        /// </summary>
        public bool TopTier_3 { get; }

        /// <summary>
        /// Top tier 2 tower.
        /// </summary>
        public bool TopTier_2 { get; }

        /// <summary>
        /// Top tier 1 tower
        /// </summary>
        public bool TopTier_1 { get; }

        /// <summary>
        /// Instantiates TowerStatus object from 16-bit unsigned integer.
        /// </summary>
        /// <param name="bits">Tower status value compressed to 32-bit integer</param>
        // ┌─┬─┬─┬─┬─────────────────────── not used.
        // │ │ │ │ │ ┌───────────────────── ancient Bottom
        // │ │ │ │ │ │ ┌─────────────────── ancient Top
        // │ │ │ │ │ │ │ ┌───────────────── bottom Tier 3
        // │ │ │ │ │ │ │ │ ┌─────────────── bottom Tier 2
        // │ │ │ │ │ │ │ │ │ ┌───────────── bottom Tier 1
        // │ │ │ │ │ │ │ │ │ │ ┌─────────── middle Tier 3
        // │ │ │ │ │ │ │ │ │ │ │ ┌───────── middle Tier 2
        // │ │ │ │ │ │ │ │ │ │ │ │ ┌─────── middle Tier 1
        // │ │ │ │ │ │ │ │ │ │ │ │ │ ┌───── top Tier 3
        // │ │ │ │ │ │ │ │ │ │ │ │ │ │ ┌─── top Tier 2
        // │ │ │ │ │ │ │ │ │ │ │ │ │ │ │ ┌─ top Tier 1
        // 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
        public TowerStatus(int value)
        {
            BitVector32 bits = new BitVector32(value);

            AncientTop = bits[1024];
            AncientBottom = bits[512];
            BottomTier_3 = bits[256];
            BottomTier_2 = bits[128];
            BottomTier_1 = bits[64];
            MiddleTier_3 = bits[32];
            MiddleTier_2 = bits[16];
            MiddleTier_1 = bits[8];
            TopTier_3 = bits[4];
            TopTier_2 = bits[2];
            TopTier_1 = bits[1];
        }
    }
}
