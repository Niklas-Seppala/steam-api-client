using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public sealed class MatchDetails
    {
        public uint Duration { get; set; }
        [JsonProperty("pre_game_duration")]
        public uint PreGameDuration { get; set; }
        [JsonProperty("start_time")]
        public ulong StartTime { get; set; }
        [JsonProperty("first_blood_time")]
        public uint FirstBloodTime { get; set; }
        [JsonConverter(typeof(MapStateConverter))]
        [JsonProperty("tower_status_radiant")]
        public TowerStatus TowerStatusRadiant { get; set; }
        [JsonConverter(typeof(MapStateConverter))]
        [JsonProperty("tower_status_dire")]
        public TowerStatus TowerStatusDire { get; set; }
        [JsonConverter(typeof(MapStateConverter))]
        [JsonProperty("barracks_status_radiant")]
        public BarracksStatus BarracksStatusRadiant { get; set; }
        [JsonConverter(typeof(MapStateConverter))]
        [JsonProperty("barracks_status_dire")]
        public BarracksStatus BarracksStatusDire { get; set; }
        [JsonProperty("match_id")]
        public ulong MatchId { get; set; }
        [JsonProperty("match_seq_num")]
        public ulong MatchSequenceNum { get; set; }
        [JsonProperty("radiant_win")]
        public bool RadiantWin { get; set; }
        public bool DireWin => !RadiantWin;
        public ulong Cluster { get; set; }
        [JsonProperty("positive_votes")]
        public uint PositiveVotes { get; set; }
        [JsonProperty("negative_votes")]
        public uint NegativeVotes { get; set; }
        [JsonProperty("human_players")]
        public byte HumanPlayers { get; set; }
        [JsonProperty("lobby_type")]
        public byte LobbyType { get; set; }
        [JsonProperty("leagueid")]
        public uint LeagueId { get; set; }
        [JsonProperty("game_mode")]
        public uint GameMode { get; set; }
        [JsonProperty("radiant_score")]
        public uint RadiantScore { get; set; }
        public uint Engine { get; set; }
        [JsonProperty("dire_score")]
        public uint DireScore { get; set; }
        [JsonProperty("picks_bans")]
        public IReadOnlyList<Draft> DraftPhase { get; set; }
        public IReadOnlyList<Player> Players { get; set; }
    }
}
