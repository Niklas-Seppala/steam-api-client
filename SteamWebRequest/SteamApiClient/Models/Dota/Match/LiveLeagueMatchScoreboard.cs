using Newtonsoft.Json;

namespace SteamApiClient.Models.Dota
{
    public class LiveLeagueMatchScoreboard
    {
        public double Duration { get; set; }

        [JsonProperty("roshan_respawn_timer")]
        public ushort RoshanRespawnTimer { get; set; }

        public LiveLeagueMatchScoreboardTeam Dire { get; set; }
        public LiveLeagueMatchScoreboardTeam Radiant { get; set; }
    }
}
