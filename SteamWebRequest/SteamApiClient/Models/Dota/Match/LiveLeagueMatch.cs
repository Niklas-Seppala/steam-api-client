﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{

    public class LiveLeagueMatch
    {
        [JsonProperty("lobby_id")]
        public ulong LobbyId { get; set; }
        [JsonProperty("match_id")]
        public ulong MatchId { get; set; }
        public uint Spectators { get; set; }
        [JsonProperty("league_id")]
        public uint LeagueId { get; set; }
        [JsonProperty("league_node_id")]
        public uint LeagueNodeId { get; set; }
        [JsonProperty("steam_delay_s")]
        public uint StreamDelay { get; set; }
        [JsonProperty("radiant_series_wins")]
        public uint RadiantSeriesWins { get; set; }
        [JsonProperty("dire_series_wins")]
        public uint DireSeriesWins { get; set; }
        [JsonProperty("series_type")]
        public uint SeriesType { get; set; }
        [JsonProperty("radiant_team")]
        public LiveLeagueMatchTeam RadiantTeam { get; set; }
        [JsonProperty("dire_team")]
        public LiveLeagueMatchTeam DireTeam { get; set; }
        public IReadOnlyList<LiveLeagueMatchPlayer> Players { get; set; }
        public LiveLeagueMatchScoreboard Scoreboard { get; set; }
    }
}
