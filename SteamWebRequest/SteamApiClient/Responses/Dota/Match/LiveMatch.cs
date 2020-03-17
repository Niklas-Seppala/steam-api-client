using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Live match model
    /// </summary>
    [Serializable]
    public sealed class LiveMatch
    {
        [JsonProperty("activate_time")]
        public ulong ActivateTime { get; set; }

        [JsonProperty("deactivate_time")]
        public ulong DeactivateTime { get; set; }

        /// <summary>
        /// Match server id
        /// </summary>
        [JsonProperty("server_steam_id")]
        public ulong ServerSteamId { get; set; }

        /// <summary>
        /// Lobby id
        /// </summary>
        [JsonProperty("lobby_id")]
        public ulong LobbyId { get; set; }

        /// <summary>
        /// League id
        /// </summary>
        [JsonProperty("league_id")]
        public ulong LeagueId { get; set; }

        /// <summary>
        /// Type of the lobby
        /// </summary>
        [JsonProperty("lobby_type")]
        public uint LobbyType { get; set; }

        /// <summary>
        /// Game time in seconds
        /// </summary>
        [JsonProperty("game_time")]
        public int GameTime { get; set; }

        /// <summary>
        /// Game mode
        /// </summary>
        [JsonProperty("game_mode")]
        public uint GameMode { get; set; }

        /// <summary>
        /// Average MMR in live game
        /// </summary>
        [JsonProperty("average_mmr")]
        public uint AverageMMR { get; set; }

        /// <summary>
        /// Match id
        /// </summary>
        [JsonProperty("match_id")]
        public ulong MatchId { get; set; }

        /// <summary>
        /// Id of the series
        /// </summary>
        [JsonProperty("series_id")]
        public ulong SeriesId { get; set; }

        /// <summary>
        /// Radiant team name
        /// </summary>
        [JsonProperty("team_name_radiant", NullValueHandling = NullValueHandling.Ignore)]
        public string TeamNameRadiant { get; set; }

        /// <summary>
        /// Dire team name
        /// </summary>
        [JsonProperty("team_name_dire", NullValueHandling = NullValueHandling.Ignore)]
        public string TeamNameDire { get; set; }

        /// <summary>
        /// Radiant team logo. Can be null.
        /// </summary>
        [JsonProperty("team_logo_radiant", NullValueHandling = NullValueHandling.Ignore)]
        public ulong? TeamLogoRadiant { get; set; }

        /// <summary>
        /// Dire team logo. Can be null
        /// </summary>
        [JsonProperty("team_logo_dire", NullValueHandling = NullValueHandling.Ignore)]
        public ulong? TeamLogoDire { get; set; }

        /// <summary>
        /// Radiant team id. Can be null
        /// </summary>
        [JsonProperty("team_id_radiant", NullValueHandling = NullValueHandling.Ignore)]
        public ulong? TeamIdRadiant { get; set; }

        /// <summary>
        /// Dire team id. Can be null
        /// </summary>
        [JsonProperty("team_id_dire", NullValueHandling = NullValueHandling.Ignore)]
        public ulong? TeamIdDire { get; set; }

        /// <summary>
        /// Sort score
        /// </summary>
        [JsonProperty("sort_score")]
        public uint SortScore { get; set; }

        /// <summary>
        /// Unixtimestamp of the last time live match was updated
        /// </summary>
        [JsonProperty("last_update_time")]
        public ulong LastUpdateTime { get; set; }

        /// <summary>
        /// Radiant team lead in networth.
        /// Negative means Dire team leads.
        /// </summary>
        [JsonProperty("radiant_lead")]
        public int RadiantLead { get; set; }

        /// <summary>
        /// Radiant team score
        /// </summary>
        [JsonProperty("radiant_score")]
        public uint RadiantScore { get; set; }

        /// <summary>
        /// Dire team score
        /// </summary>
        [JsonProperty("dire_score")]
        public uint DireScore { get; set; }

        /// <summary>
        /// List of the players
        /// </summary>
        [JsonProperty("players")]
        public IReadOnlyList<PlayerShort> Players { get; set; }

        /// <summary>
        /// Building state
        /// </summary>
        [JsonProperty("building_state")]
        public uint BuildingState { get; set; }

        /// <summary>
        /// Live match delay in seconds
        /// </summary>
        public uint Delay { get; set; }

        /// <summary>
        /// Live match spectator count
        /// </summary>
        public uint Spectators { get; set; }
    }
}
