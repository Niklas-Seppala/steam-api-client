using Newtonsoft.Json;
using System;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Dota 2 pro team in pro tournament
    /// </summary>
    [Serializable]
    public sealed class TournamentTeam
    {
        /// <summary>
        /// Pro dota 2 team id
        /// </summary>
        public uint TeamId { get; set; }

        /// <summary>
        /// Pro dota 2 team name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Amount won so far
        /// </summary>
        public double Payout { get; set; }

        /// <summary>
        /// Count of games won so far
        /// </summary>
        [JsonProperty("games_won")]
        public uint GamesWon { get; set; }

        /// <summary>
        /// Team's logo URL
        /// </summary>
        [JsonProperty("url_logo")]
        public string UrlLogo { get; set; }
    }
}
