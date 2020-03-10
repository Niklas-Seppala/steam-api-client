namespace SteamApi.Utility
{
    /// <summary>
    /// Static utility class that expands steam api response's
    /// compressed values.
    /// </summary>
    public static class ExpandValues
    {
        /// <summary>
        /// Decompresses player slot value from integer value
        /// </summary>
        /// <param name="playerSlotBitValue">player slot compressed to integer value</param>
        /// <returns>PlayerSlot object</returns>
        // ┌─────────────── team (false if Radiant, true if Dire).
        // │ ┌─┬─┬─┬─────── not used.
        // │ │ │ │ │ ┌─┬─┬─ the position of a player within their team (0-4).
        // │ │ │ │ │ │ │ │
        // 0 0 0 0 0 0 0 0
        public static PlayerSlot GetPlayerSlot(int playerSlotBitValue)
        {
            return new PlayerSlot(playerSlotBitValue);
        }


        /// <summary>
        /// Decompresses tower status value from integer value.
        /// </summary>
        /// <param name="towerStatusBitValue">tower status compressed to integer value</param>
        /// <returns>TowerStatus object</returns>
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
        public static TowerStatus GetTowerStatus(int towerStatusBitValue)
        {
            return new TowerStatus(towerStatusBitValue);
        }


        /// <summary>
        /// Decompresses barracks status value from integer value
        /// </summary>
        /// <param name="barracksStatusBitValue">barracks status compressed to integer value</param>
        /// <returns>BarrackStatus object</returns>
        // ┌─┬───────────── not used.
        // │ │ ┌─────────── bottom Ranged
        // │ │ │ ┌───────── bottom Melee
        // │ │ │ │ ┌─────── middle Ranged
        // │ │ │ │ │ ┌───── middle Melee
        // │ │ │ │ │ │ ┌─── top Ranged
        // │ │ │ │ │ │ │ ┌─ top Melee
        // 0 0 0 0 0 0 0 0
        public static BarracksStatus GetBarrackStatus(int barracksStatusBitValue)
        {
            return new BarracksStatus(barracksStatusBitValue);
        }
    }
}
