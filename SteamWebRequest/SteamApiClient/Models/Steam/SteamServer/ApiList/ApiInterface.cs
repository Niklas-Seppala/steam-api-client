using System.Collections.Generic;

namespace SteamApi.Models.Steam
{
    /// <summary>
    /// Steam API interface
    /// </summary>
    public sealed class ApiInterface
    {
        /// <summary>
        /// API interface name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// List of interface methods
        /// </summary>
        public IReadOnlyList<Method> Methods { get; set; }
    }
}
