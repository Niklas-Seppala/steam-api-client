using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public sealed class MatchDetails
    {
        [JsonProperty("result")]
        public MatchDetailsContent Details { get; set; }
    }

    public sealed class MatchDetailsContent
    {
        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan Duration { get; set; }

        [JsonConverter(typeof(TimeSpanConverter))]
        [JsonProperty("pre_game_duration")]
        public TimeSpan PreGameDuration { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty("start_time")]
        public DateTime StartTime { get; set; }

        [JsonConverter(typeof(TimeSpanConverter))]
        [JsonProperty("first_blood_time")]
        public TimeSpan FirstBloodTime { get; set; }

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

        public bool DireWin => !this.RadiantWin;

        [JsonProperty("human_players")]
        public byte HumanPlayers { get; set; }

        [JsonProperty("lobby_type")]
        public byte LobbyType { get; set; }

        [JsonProperty("League_id")]
        public byte LeagueId { get; set; }

        [JsonProperty("game_mode")]
        public byte GameMode { get; set; }

        [JsonProperty("radiant_score")]
        public ushort RadiantScore { get; set; }

        [JsonProperty("dire_score")]
        public ushort DireScore { get; set; }

        public List<Player> Players { get; set; }
        public string[] PlayerIds32
        {
            get
            {
                string[] ids = new string[this.Players.Count];
                for (int i = 0; i < this.Players.Count; i++)
                {
                    ids[i] = this.Players[i].Id32.ToString();
                }
                return ids;
            }
        }

        public string[] PlayerIds64
        {
            get
            {
                string[] ids = new string[this.Players.Count];
                for (int i = 0; i < this.Players.Count; i++)
                {
                    ids[i] = this.Players[i].Id64.ToString();
                }
                return ids;
            }
        }
    }
}
