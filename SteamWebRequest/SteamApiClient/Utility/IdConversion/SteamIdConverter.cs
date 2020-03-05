using System;

namespace SteamApi
{
    /// <summary>
    /// Class that provides static functions for
    /// Steam id conversions between 32-bit and 64-bit ids.
    /// </summary>
    public static class SteamIdConverter
    {
        /// <summary>
        /// Converts 32-bit Steam id to 64-bit version.
        /// </summary>
        /// <param name="id32Str">32-bit steam id as a string</param>
        public static string SteamIdTo64(string id32Str)
        {
            if (uint.TryParse(id32Str, out uint id32))
            {
                checked { return (id32 + 76561197960265728).ToString(); }
            }
            throw new ArgumentException("Given string couldn't be parsed to UInt32.");
        }

        /// <summary>
        /// Converts 64-bit Steam id to 32-bit version.
        /// </summary>
        /// <param name="id64Str">64-bit Steam id as a string</param>
        public static string SteamIdTo32(string id64Str)
        {
            ulong start = 76561197960265728;
            if (ulong.TryParse(id64Str, out ulong id64))
            {
                if (id64 < start)
                    throw new ArgumentOutOfRangeException("Id is not valid 64-bit steam id.");
                checked { return (id64 - start).ToString(); }
            }
            throw new ArgumentException("Given string couldn't be parsed to UInt64");
        }

        /// <summary>
        /// Converts 32-bit integer Steam Id to 64-bit integer id. 
        /// </summary>
        /// <param name="id32">32-bit Steam id</param>
        public static ulong SteamIdTo64(uint id32)
        {
            checked { return (ulong)id32 + 76561197960265728; }
        }

        /// <summary>
        /// Converts 64-bit integer Steam id to 32-bit integer id.
        /// </summary>
        /// <param name="id64">64-bit Steam id</param>
        public static uint SteamIdTo32(ulong id64)
        {
            ulong start = 76561197960265728;
            if (id64 < start)
                throw new ArgumentOutOfRangeException("Id is not valid 64-bit steam id.");
            checked { return (uint)(id64 - start); }
        }
    }
}
