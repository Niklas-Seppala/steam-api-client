using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace SteamWebRequest
{
    public sealed class LiveMatch
    {
        [JsonProperty("activate_time")]
        public long ActivateTime { get; set; }

        [JsonProperty("deactivate_time")]
        public long DeactivateTime { get; set; }

        [JsonProperty("server_steam_id")]
        public double ServerSteamId { get; set; }

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
        public short AverageMmr { get; set; }

        [JsonProperty("match_id")]
        public long MatchId { get; set; }

        [JsonProperty("series_id")]
        public long SeriesId { get; set; }

        [JsonProperty("team_name_radiant", NullValueHandling = NullValueHandling.Ignore)]
        public string TeamNameRadiant { get; set; }

        [JsonProperty("team_name_dire", NullValueHandling = NullValueHandling.Ignore)]
        public string TeamNameDire { get; set; }

        [JsonProperty("team_logo_radiant", NullValueHandling = NullValueHandling.Ignore)]
        public uint? TeamLogoRadiant { get; set; }

        [JsonProperty("team_logo_dire", NullValueHandling = NullValueHandling.Ignore)]
        public uint? TeamLogoDire { get; set; }

        [JsonProperty("team_id_radiant", NullValueHandling = NullValueHandling.Ignore)]
        public uint? TeamIdRadiant { get; set; }

        [JsonProperty("team_id_dire", NullValueHandling = NullValueHandling.Ignore)]
        public uint? TeamIdDire { get; set; }

        [JsonProperty("sort_score")]
        public int SortScore { get; set; }

        [JsonProperty("last_update_time")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public long LastUpdateTime { get; set; }

        [JsonProperty("radiant_lead")]
        public short RadiantLead { get; set; }

        [JsonProperty("radiant_score")]
        public short RadiantScore { get; set; }

        [JsonProperty("dire_score")]
        public short DireScore { get; set; }

        [JsonProperty("players")]
        public List<Player> Players { get; set; }

        [JsonProperty("building_state")]
        public long BuildingState { get; set; }

        public long Delay { get; set; }
        public long Spectators { get; set; }
    }
}
