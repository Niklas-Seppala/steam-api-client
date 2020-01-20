using System.Collections.Generic;
using Newtonsoft.Json;

namespace SteamWebRequest.Models
{
    public sealed class Match
    {
        [JsonProperty("match_id")]
        public ulong MatchId { get; set; }

        [JsonProperty("match_seq_num")]
        public ulong SequenceNumber { get; set; }

        [JsonProperty("start_time")]
        public ulong StartTime { get; set; }

        [JsonProperty("lobby_type")]
        public ushort LobbyType { get; set; }

        [JsonProperty("radiant_team_id")]
        public uint RadiantTeamId { get; set; }

        [JsonProperty("dire_team_id")]
        public uint DireTeamId { get; set; }

        public List<PlayerShort> Players { get; set; }
    }

}
