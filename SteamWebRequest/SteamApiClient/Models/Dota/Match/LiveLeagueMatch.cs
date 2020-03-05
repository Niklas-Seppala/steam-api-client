using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Dota 2 live league match model
    /// </summary>
    public class LiveLeagueMatch
    {
        /// <summary>
        /// Id of the lobby
        /// </summary>
        [JsonProperty("lobby_id")]
        public ulong LobbyId { get; set; }

        /// <summary>
        /// Match id
        /// </summary>
        [JsonProperty("match_id")]
        public ulong MatchId { get; set; }

        /// <summary>
        /// Number of spectators
        /// </summary>
        public uint Spectators { get; set; }

        /// <summary>
        /// Id of the league
        /// </summary>
        [JsonProperty("league_id")]
        public uint LeagueId { get; set; }

        /// <summary>
        /// Id of the node in league
        /// </summary>
        [JsonProperty("league_node_id")]
        public uint LeagueNodeId { get; set; }

        /// <summary>
        /// Stream delay in seconds
        /// </summary>
        [JsonProperty("steam_delay_s")]
        public uint StreamDelay { get; set; }

        /// <summary>
        /// Number of Radiant team wins in current series
        /// </summary>
        [JsonProperty("radiant_series_wins")]
        public uint RadiantSeriesWins { get; set; }

        /// <summary>
        /// Number of Dire team wins in current series
        /// </summary>
        [JsonProperty("dire_series_wins")]
        public uint DireSeriesWins { get; set; }

        /// <summary>
        /// Type of the series
        /// </summary>
        [JsonProperty("series_type")]
        public uint SeriesType { get; set; }

        /// <summary>
        /// Radiant team model
        /// </summary>
        [JsonProperty("radiant_team")]
        public LiveLeagueMatchTeam RadiantTeam { get; set; }

        /// <summary>
        /// Dire team model
        /// </summary>
        [JsonProperty("dire_team")]
        public LiveLeagueMatchTeam DireTeam { get; set; }

        /// <summary>
        /// Players in current match
        /// </summary>
        public IReadOnlyList<LiveLeagueMatchPlayer> Players { get; set; }

        /// <summary>
        /// Current match scoreboard
        /// </summary>
        public LiveLeagueMatchScoreboard Scoreboard { get; set; }
    }
}
