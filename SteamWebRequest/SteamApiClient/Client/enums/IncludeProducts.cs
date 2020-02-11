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
        None = 0,
        Games = 1,
        DLC = 2,
        Software = 4,
        Harware = 8,
        Videos = 16,

        Media = Games | DLC | Videos,

        GameProducs = Games | DLC,

        All = Games | DLC | Software | Harware | Videos
    }
}
