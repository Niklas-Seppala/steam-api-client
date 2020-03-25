using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Dota 2 tournament prize pool.
    /// </summary>
    [Serializable]
    public sealed class TournamentPrizePool
    {
        /// <summary>
        /// Dota 2 tournament prize pool in dollars.
        /// </summary>
        [JsonProperty("prize_pool")]
        public uint PrizePool { get; set; }


        /// <summary>
        /// Dota 2 tournament id.
        /// </summary>
        [JsonProperty("league_id")]
        public uint LeagueId { get; set; }

        /// <summary>
        /// Response status.
        /// </summary>
        public uint Status { get; set; }
    }
}
