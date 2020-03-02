using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class TournamentTeam
    {
        public uint TeamId { get; set; }
        public string Name { get; set; }
        public double Payout { get; set; }

        [JsonProperty("games_won")]
        public uint GamesWon { get; set; }

        [JsonProperty("url_logo")]
        public string UrlLogo { get; set; }
    }
}
