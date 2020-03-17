using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Pro dota league match node
    /// </summary>
    [Serializable]
    public sealed class LeagueNode
    {
        /// <summary>
        /// Node name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Node id
        /// </summary>
        [JsonProperty("node_id")]
        public ulong NodeId { get; set; }

        /// <summary>
        /// Node group id
        /// </summary>
        [JsonProperty("node_group_id")]
        public ulong NodeGroupId { get; set; }

        /// <summary>
        /// Winnig node id
        /// </summary>
        [JsonProperty("winnig_node_id")]
        public ulong WinningNodeId { get; set; }

        /// <summary>
        /// Losing node id
        /// </summary>
        [JsonProperty("losing_node_id")]
        public ulong LosingNodeId { get; set; }

        [JsonProperty("incoming_node_id_1")]
        public ulong IncomingNode_1_Id { get; set; }
        [JsonProperty("incoming_node_id_2")]
        public ulong IncomingNode_2_Id { get; set; }

        /// <summary>
        /// Node type
        /// </summary>
        [JsonProperty("node_type")]
        public uint NodeType { get; set; }

        /// <summary>
        /// Unixtimestamp of the scheduled time
        /// </summary>
        [JsonProperty("scheduled_time")]
        public ulong ScheduledTime { get; set; }

        /// <summary>
        /// Unixtimestamp of the actual time
        /// </summary>
        [JsonProperty("actual_time")]
        public ulong ActualTime { get; set; }

        /// <summary>
        /// Id of the series
        /// </summary>
        [JsonProperty("series_id")]
        public ulong SeriesId { get; set; }

        /// <summary>
        /// Team 1 id
        /// </summary>
        [JsonProperty("team_id_1")]
        public ulong Team_1_Id { get; set; }

        /// <summary>
        /// Team 2 id
        /// </summary>
        [JsonProperty("team_id_2")]
        public ulong Team_2_Id { get; set; }

        /// <summary>
        /// Team 1 win count
        /// </summary>
        public uint Team_1_WinCount { get; set; }

        /// <summary>
        /// Team 2 win count
        /// </summary>
        public uint Team_2_WinCount { get; set; }

        /// <summary>
        /// Has a match started
        /// </summary>
        [JsonProperty("has_started")]
        public bool MatchStarted { get; set; }

        /// <summary>
        /// Is the match completed
        /// </summary>
        [JsonProperty("is_completed")]
        public bool MatchCompleted { get; set; }

        /// <summary>
        /// List of the stream ids
        /// </summary>
        [JsonProperty("stream_ids")]
        public IReadOnlyList<ulong> StreamIds { get; set; }

        /// <summary>
        /// List of match results
        /// </summary>
        public IReadOnlyList<LeagueMatchResult> Matches { get; set; }
    }
}
