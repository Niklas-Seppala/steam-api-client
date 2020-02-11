using System;

namespace SteamApi
{
    /// <summary>
    /// Class that provides static functions for
    /// Steam id conversions between 32-bit and 64-bit ids.
    /// </summary>
    internal static class SteamIdConverter
    {
        /// <summary>
        /// Converts 32-bit Steam id to 64-bit version.
        /// </summary>
        /// <param name="id32Str">32-bit steam id as a string</param>
        public static string SteamIdTo64(string id32Str)
        {
            if (int.TryParse(id32Str, out int id32))
            {
                if (id32 < 0)
                    throw new ArgumentOutOfRangeException("Id can't be negative.");
                checked { return (id32 + 76561197960265728).ToString(); }
            }
            throw new ArgumentException("Given string couldn't be parsed to Int32.");
        }

        /// <summary>
        /// Converts 64-bit Steam id to 32-bit version.
        /// </summary>
        /// <param name="id64Str">64-bit Steam id as a string</param>
        public static string SteamIdTo32(string id64Str)
        {
            long difference = 76561197960265728;
            if (long.TryParse(id64Str, out long id64))
            {
                if (id64 < difference)
                    throw new ArgumentOutOfRangeException("Id is not valid 64-bit id.");
                checked { return (id64 - difference).ToString(); }
            }
            throw new ArgumentException("Given string couldn't be parsed to Int64");
        }

        /// <summary>
        /// Converts 32-bit integer Steam Id to 64-bit integer id. 
        /// </summary>
        /// <param name="id32">32-bit Steam id</param>
        public static ulong SteamIdTo64(uint id32)
        {
            if (id32 < 0)
                throw new ArgumentOutOfRangeException("Id cant be negative");
            checked { return (ulong)id32 + 76561197960265728; }
        }

        /// <summary>
        /// Converts 64-bit integer Steam id to 32-bit integer id.
        /// </summary>
        /// <param name="id64">64-bit Steam id</param>
        public static uint SteamIdTo32(ulong id64)
        {
            ulong difference = 76561197960265728;
            if (id64 < difference)
                throw new ArgumentOutOfRangeException("Id is not valid 64-bit id.");
            checked { return (uint)(id64 - difference); }
        }
    }
}
