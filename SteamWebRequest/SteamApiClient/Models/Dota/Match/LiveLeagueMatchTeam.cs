using Newtonsoft.Json;

namespace SteamApiClient.Models.Dota
{
    public class LiveLeagueMatchTeam
    {
        [JsonProperty("team_name")]
        public string TeamName { get; set; }

        [JsonProperty("team_id")]
        public uint TeamId { get; set; }

        [JsonProperty("team_logo")]
        public ulong TeamLogo { get; set; }

        public bool Complete { get; set; }
    }
}
