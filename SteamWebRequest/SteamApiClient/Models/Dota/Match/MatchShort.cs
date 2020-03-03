using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class MatchShort
    {
        [JsonProperty("match_id")]
        public ulong MatchId { get; set; }
        [JsonProperty("match_seq_num")]
        public ulong SequenceNumber { get; set; }
        [JsonProperty("start_time")]
        public ulong StartTime { get; set; }
        [JsonProperty("lobby_type")]
        public uint LobbyType { get; set; }
        [JsonProperty("radiant_team_id")]
        public uint RadiantTeamId { get; set; }
        [JsonProperty("dire_team_id")]
        public uint DireTeamId { get; set; }
        public IReadOnlyList<PlayerShort> Players { get; set; }
    }
}
