using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    public class LiveLeagueMatchScoreboardTeam
    {
        public ushort Score { get; set; }

        [JsonProperty("tower_state")]
        public uint TowerState { get; set; }

        [JsonProperty("barracks_state")]
        public uint BarracksState { get; set; }

        public IReadOnlyCollection<Pick> Picks { get; set; }
        public IReadOnlyCollection<Ban> Bans { get; set; }

        public IReadOnlyCollection<LiveLeagueMatchPlayer> Players { get; set; }
        public IReadOnlyCollection<IReadOnlyDictionary<string, uint>> Abilities { get; set; }
    }
}
