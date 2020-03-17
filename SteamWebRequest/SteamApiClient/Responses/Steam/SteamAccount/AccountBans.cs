using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Steam
{
    /// <summary>
    /// Steam users ban history model.
    /// </summary>
    [Serializable]
    public sealed class AccountBans
    {
        /// <summary>
        /// 64-bit Steam id
        /// </summary>
        [JsonProperty("SteamId")]
        public ulong Id64 { get; set; }

        /// <summary>
        /// Community ban
        /// </summary>
        public bool CommunityBanned { get; set; }

        /// <summary>
        /// Valve Anti Cheat ban
        /// </summary>
        public bool VACBanned { get; set; }

        /// <summary>
        /// VAC ban history
        /// </summary>
        public uint NumberOfVACBans { get; set; }

        /// <summary>
        /// Day count since last ban
        /// </summary>
        public uint DaysSinceLastBan { get; set; }

        /// <summary>
        /// Game ban count
        /// </summary>
        public uint NumberOfGameBans { get; set; }

        /// <summary>
        /// Banned from steam economy
        /// </summary>
        public string EconomyBan { get; set; }
    }
}
