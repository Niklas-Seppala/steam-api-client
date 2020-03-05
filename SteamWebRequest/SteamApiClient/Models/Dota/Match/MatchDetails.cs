using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Match details model
    /// </summary>
    public sealed class MatchDetails
    {
        /// <summary>
        /// Game duration in seconds
        /// </summary>
        public uint Duration { get; set; }

        /// <summary>
        /// Duration of the pre game aka. draft
        /// </summary>
        [JsonProperty("pre_game_duration")]
        public uint PreGameDuration { get; set; }

        /// <summary>
        /// Unixtimestamp of the match start time
        /// </summary>
        [JsonProperty("start_time")]
        public ulong StartTime { get; set; }

        /// <summary>
        /// Timestamp of the first blood in gametime.
        /// </summary>
        [JsonProperty("first_blood_time")]
        public uint FirstBloodTime { get; set; }

        /// <summary>
        /// Radiants towerstatus
        /// </summary>
        [JsonConverter(typeof(MapStateConverter))]
        [JsonProperty("tower_status_radiant")]
        public TowerStatus TowerStatusRadiant { get; set; }

        /// <summary>
        /// Dire towerstatus
        /// </summary>
        [JsonConverter(typeof(MapStateConverter))]
        [JsonProperty("tower_status_dire")]
        public TowerStatus TowerStatusDire { get; set; }

        /// <summary>
        /// Radiant's barracks status
        /// </summary>
        [JsonConverter(typeof(MapStateConverter))]
        [JsonProperty("barracks_status_radiant")]
        public BarracksStatus BarracksStatusRadiant { get; set; }

        /// <summary>
        /// Dire's barracks status
        /// </summary>
        [JsonConverter(typeof(MapStateConverter))]
        [JsonProperty("barracks_status_dire")]
        public BarracksStatus BarracksStatusDire { get; set; }

        /// <summary>
        /// Match id
        /// </summary>
        [JsonProperty("match_id")]
        public ulong MatchId { get; set; }

        /// <summary>
        /// Match sequence number
        /// </summary>
        [JsonProperty("match_seq_num")]
        public ulong MatchSequenceNum { get; set; }

        /// <summary>
        /// Did Radiant win?
        /// </summary>
        [JsonProperty("radiant_win")]
        public bool RadiantWin { get; set; }

        /// <summary>
        /// Did Dire win?
        /// </summary>
        public bool DireWin => !RadiantWin;

        /// <summary>
        /// Cluster id
        /// </summary>
        public ulong Cluster { get; set; }

        /// <summary>
        /// Positive vote count
        /// </summary>
        [JsonProperty("positive_votes")]
        public uint PositiveVotes { get; set; }

        /// <summary>
        /// Negative vote count
        /// </summary>
        [JsonProperty("negative_votes")]
        public uint NegativeVotes { get; set; }

        /// <summary>
        /// Count of the human players in the match
        /// </summary>
        [JsonProperty("human_players")]
        public uint HumanPlayers { get; set; }

        /// <summary>
        /// Type of the lobby
        /// </summary>
        [JsonProperty("lobby_type")]
        public uint LobbyType { get; set; }

        /// <summary>
        /// League id
        /// </summary>
        [JsonProperty("leagueid")]
        public uint LeagueId { get; set; }

        /// <summary>
        /// Game mode
        /// </summary>
        [JsonProperty("game_mode")]
        public uint GameMode { get; set; }

        /// <summary>
        /// Radiant score
        /// </summary>
        [JsonProperty("radiant_score")]
        public uint RadiantScore { get; set; }

        /// <summary>
        /// Source game engine version
        /// </summary>
        public uint Engine { get; set; }

        /// <summary>
        /// Dire score
        /// </summary>
        [JsonProperty("dire_score")]
        public uint DireScore { get; set; }

        /// <summary>
        /// List of the drafting
        /// </summary>
        [JsonProperty("picks_bans")]
        public IReadOnlyList<Draft> DraftPhase { get; set; }

        /// <summary>
        /// List of the match players
        /// </summary>
        public IReadOnlyList<Player> Players { get; set; }
    }
}
