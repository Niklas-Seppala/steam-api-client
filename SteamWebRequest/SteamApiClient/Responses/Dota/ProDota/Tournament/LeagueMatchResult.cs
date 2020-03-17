using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Pro dota league match result model
    /// </summary>
    [Serializable]
    public sealed class LeagueMatchResult
    {
        /// <summary>
        /// Match id
        /// </summary>
        [JsonProperty("match_id")]
        public ulong MatchId { get; set; }

        /// <summary>
        /// Id of the winner team
        /// </summary>
        [JsonProperty("winning_team_id")]
        public ulong WinnerTeamId { get; set; }
    }
}
