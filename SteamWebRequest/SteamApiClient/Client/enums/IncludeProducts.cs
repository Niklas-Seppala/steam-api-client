using System;

namespace SteamApi
{
    /// <summary>
    /// Included products from Steam store.
    /// May be used as a set.
    /// </summary>
    [Flags]
    public enum IncludeProducts : byte
    {
        /// <summary>
        /// No products included
        /// </summary>
        None = 0,

        /// <summary>
        /// Game products included
        /// </summary>
        Games = 1,

        /// <summary>
        /// DLC products included
        /// </summary>
        DLC = 2,

        /// <summary>
        /// Software products included
        /// </summary>
        Software = 4,

        /// <summary>
        /// Hardware products included
        /// </summary>
        Harware = 8,

        /// <summary>
        /// Videos included
        /// </summary>
        Videos = 16,

        /// <summary>
        /// Entertainment media products included
        /// </summary>
        Media = Games | DLC | Videos,

        /// <summary>
        /// Games and DLCs included
        /// </summary>
        GameProducs = Games | DLC,

        /// <summary>
        /// All products included
        /// </summary>
        All = Games | DLC | Software | Harware | Videos
    }
}
