using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class LiveLeagueMatchScoreboardTeam
    {
        public uint Score { get; set; }
        [JsonProperty("tower_state")]
        public uint TowerState { get; set; }
        [JsonProperty("barracks_state")]
        public uint BarracksState { get; set; }
        public IReadOnlyList<Pick> Picks { get; set; }
        public IReadOnlyList<Ban> Bans { get; set; }
        public IReadOnlyList<LiveLeagueMatchPlayer> Players { get; set; }
        public IReadOnlyList<IReadOnlyDictionary<string, uint>> Abilities { get; set; }
    }
}
