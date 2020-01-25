using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public sealed class Match
    {
        [JsonProperty("match_id")]
        public ulong MatchId { get; set; }

        [JsonProperty("match_seq_num")]
        public ulong SequenceNumber { get; set; }

        [JsonProperty("start_time")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime StartTime { get; set; }

        [JsonProperty("lobby_type")]
        public ushort LobbyType { get; set; }

        [JsonProperty("radiant_team_id")]
        public uint RadiantTeamId { get; set; }

        [JsonProperty("dire_team_id")]
        public uint DireTeamId { get; set; }

        public List<PlayerShort> Players { get; set; }
    }
}
