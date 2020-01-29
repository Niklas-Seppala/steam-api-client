using Newtonsoft.Json;

namespace SteamApiClient.Models
{
    internal class HeroesResponse
    {
        [JsonProperty("result")]
        public HeroesContent Content { get; set; }
    }
}
