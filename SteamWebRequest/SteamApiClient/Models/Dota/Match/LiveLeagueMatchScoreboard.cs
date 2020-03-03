using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class LiveLeagueMatchScoreboard
    {
        public double Duration { get; set; }
        [JsonProperty("roshan_respawn_timer")]
        public uint RoshanRespawnTimer { get; set; }
        public LiveLeagueMatchScoreboardTeam Dire { get; set; }
        public LiveLeagueMatchScoreboardTeam Radiant { get; set; }
    }
}
