using Newtonsoft.Json;
using System;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Dota 2 team member
    /// </summary>
    [Serializable]
    public sealed class DotaTeamMember
    {
        /// <summary>
        /// Account id
        /// </summary>
        [JsonProperty("account_id")]
        public uint AccountId { get; set; }

        /// <summary>
        /// Unixtimestamp of date when joined
        /// </summary>
        [JsonProperty("time_joined")]
        public ulong TimeJoined { get; set; }
        
        /// <summary>
        /// Is team admin
        /// </summary>
        public bool Admin { get; set; }
    }
}
