using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class RealTimeMatchTeam
    {
        [JsonProperty("team_number")]
        public byte TeamNumber { get; set; }

        [JsonProperty("team_id")]
        public uint TeamId { get; set; }

        [JsonProperty("team_name")]
        public string TeamName { get; set; }

        [JsonProperty("team_tag")]
        public string TeamTag { get; set; }

        [JsonProperty("team_logo")]
        public ulong TeamLogo { get; set; }

        public uint Score { get; set; }

        [JsonProperty("net_worth")]
        public uint NetWorth { get; set; }

        [JsonProperty("team_logo_url")]
        public string TeamLogoUrl { get; set; }

        public IReadOnlyCollection<RealTimeMatchPlayer> Players { get; set; }
    }
}
