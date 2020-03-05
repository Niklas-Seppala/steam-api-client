using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Recent DCP events model
    /// </summary>
    public class RecentDcpEvents
    {
        /// <summary>
        /// List of DCP tournaments
        /// </summary>
        public IReadOnlyList<Tournament> Tournaments { get; set; }

        /// <summary>
        /// Wager unixtimestamp
        /// </summary>
        [JsonProperty("wager_timestamp")]
        public ulong WagerTimestamp { get; set; }
    }
}
