using Newtonsoft.Json;
using System;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Pro Dota 2 DCP results
    /// </summary>
    [Serializable]
    public sealed class DcpResults
    {
        /// <summary>
        /// Id of the DCP event
        /// </summary>
        [JsonProperty("league_id")]
        public uint LeagueId { get; set; }

        /// <summary>
        /// Standing in the tournament
        /// </summary>
        public uint Standing { get; set; }

        /// <summary>
        /// Points gained
        /// </summary>
        public uint Points { get; set; }

        /// <summary>
        /// How much money won
        /// </summary>
        public double Earnings { get; set; }

        /// <summary>
        /// Unixtimestamp of the event
        /// </summary>
        public ulong Timestamp { get; set; }
    }
}
