using Newtonsoft.Json;

namespace SteamApiClient.Models.Dota
{
    public class League
    {
        [JsonProperty("tournament_url")]
        public string Url { get; set; }

        public uint ItemDef { get; set; }
        public string Name { get; set; }
        public uint LeagueId { get; set; }
        public string Description { get; set; }
    }
}
