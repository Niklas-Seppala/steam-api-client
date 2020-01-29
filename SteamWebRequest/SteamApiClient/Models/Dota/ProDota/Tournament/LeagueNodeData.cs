using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    public class LeagueNode
    {
        public string Name { get; set; }

        [JsonProperty("node_id")]
        public ulong NodeId { get; set; }

        [JsonProperty("node_group_id")]
        public ulong NodeGroupId { get; set; }

        [JsonProperty("winnig_node_id")]
        public ulong WinningNodeId { get; set; }

        [JsonProperty("losing_node_id")]
        public ulong LosingNodeId { get; set; }

        [JsonProperty("incoming_node_id_1")]
        public ulong IncomingNode_1_Id { get; set; }

        [JsonProperty("incoming_node_id_2")]
        public ulong IncomingNode_2_Id { get; set; }

        [JsonProperty("node_type")]
        public uint NodeType { get; set; }

        [JsonProperty("scheduled_time")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime ScheduledTime { get; set; }

        [JsonProperty("actual_time")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime ActualTime { get; set; }

        [JsonProperty("series_id")]
        public ulong SeriesId { get; set; }

        [JsonProperty("team_id_1")]
        public ulong Team_1_Id { get; set; }

        [JsonProperty("team_id_2")]
        public ulong Team_2_Id { get; set; }

        public byte Team_1_WinCount { get; set; }
        public byte Team_2_WinCount { get; set; }

        [JsonProperty("has_started")]
        public bool MatchStarted { get; set; }

        [JsonProperty("is_completed")]
        public bool MatchCompleted { get; set; }

        [JsonProperty("stream_ids")]
        public ulong[] StreamIds { get; set; }

        public IReadOnlyCollection<LeagueMatchResult> Matches { get; set; }
    }
}
