using Newtonsoft.Json;

namespace SteamApiClient.Models.Dota
{
    internal class HeroesResponse
    {
        [JsonProperty("result")]
        public HeroesContent Content { get; set; }
    }
}
