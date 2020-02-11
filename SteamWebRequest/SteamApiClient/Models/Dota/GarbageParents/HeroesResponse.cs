using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    internal class HeroesResponse
    {
        [JsonProperty("result")]
        public HeroesContent Content { get; set; }
    }
}
