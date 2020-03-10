using System;
using System.Collections.Specialized;

namespace SteamApi
{
    /// <summary>
    /// Player's data about how they fit in their team.
    /// </summary>
    [Serializable]
    public readonly struct PlayerSlot
    {
        /// <summary>
        /// Player's position in their team
        /// </summary>
        public uint TeamPosition { get; }

        /// <summary>
        /// Is player dire
        /// </summary>
        public bool IsDire { get; }

        /// <summary>
        /// Instantiates readonly PLayerSlot object from BitVector32
        /// </summary>
        /// <param name="bits">Player slot value compressed to 32-bit integer</param>
        // ┌─────────────── team (false if Radiant, true if Dire).
        // │ ┌─┬─┬─┬─────── not used.
        // │ │ │ │ │ ┌─┬─┬─ the position of a player within their team (0-4).
        // │ │ │ │ │ │ │ │
        // 0 0 0 0 0 0 0 0
        public PlayerSlot(int value)
        {
            BitVector32 bits = new BitVector32(value);
            TeamPosition = (uint)(bits[BitVector32.CreateSection(4)] + 1);
            IsDire = bits[128];
        }
    }
}
