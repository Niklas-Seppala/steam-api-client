using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class DotaTeamMember
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
