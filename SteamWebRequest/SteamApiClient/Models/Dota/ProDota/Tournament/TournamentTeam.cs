using Newtonsoft.Json;

namespace SteamApiClient.Models.Dota
{
    public class TournamentTeam
    {
        public int TeamId { get; set; }
        public string Name { get; set; }
        public double Payout { get; set; }

        [JsonProperty("games_won")]
        public byte GamesWon { get; set; }

        [JsonProperty("url_logo")]
        public string UrlLogo { get; set; }
    }
}
