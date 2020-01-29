using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    public class LiveMatch
    {
        [JsonProperty("activate_time")]
        public ulong ActivateTime { get; set; }

        [JsonProperty("deactivate_time")]
        public ulong DeactivateTime { get; set; }

        [JsonProperty("server_steam_id")]
        public ulong ServerSteamId { get; set; }

        [JsonProperty("lobby_id")]
        public ulong LobbyId { get; set; }

        [JsonProperty("league_id")]
        public long LeagueId { get; set; }

        [JsonProperty("lobby_type")]
        public byte LobbyType { get; set; }

        [JsonProperty("game_time")]
        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan GameTime { get; set; }

        [JsonProperty("game_mode")]
        public long GameMode { get; set; }

        [JsonProperty("average_mmr")]
        public ushort AverageMMR { get; set; }

        [JsonProperty("match_id")]
        public ulong MatchId { get; set; }

        [JsonProperty("series_id")]
        public ulong SeriesId { get; set; }

        [JsonProperty("team_name_radiant", NullValueHandling = NullValueHandling.Ignore)]
        public string TeamNameRadiant { get; set; }

        [JsonProperty("team_name_dire", NullValueHandling = NullValueHandling.Ignore)]
        public string TeamNameDire { get; set; }

        [JsonProperty("team_logo_radiant", NullValueHandling = NullValueHandling.Ignore)]
        public ulong? TeamLogoRadiant { get; set; }

        [JsonProperty("team_logo_dire", NullValueHandling = NullValueHandling.Ignore)]
        public ulong? TeamLogoDire { get; set; }

        [JsonProperty("team_id_radiant", NullValueHandling = NullValueHandling.Ignore)]
        public ulong? TeamIdRadiant { get; set; }

        [JsonProperty("team_id_dire", NullValueHandling = NullValueHandling.Ignore)]
        public ulong? TeamIdDire { get; set; }

        [JsonProperty("sort_score")]
        public uint SortScore { get; set; }

        [JsonProperty("last_update_time")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime LastUpdateTime { get; set; }

        [JsonProperty("radiant_lead")]
        public int RadiantLead { get; set; }

        [JsonProperty("radiant_score")]
        public short RadiantScore { get; set; }

        [JsonProperty("dire_score")]
        public short DireScore { get; set; }

        [JsonProperty("players")]
        public IReadOnlyCollection<PlayerShort> Players { get; set; }

        [JsonProperty("building_state")]
        public uint BuildingState { get; set; }

        public ushort Delay { get; set; }
        public uint Spectators { get; set; }
    }
}
