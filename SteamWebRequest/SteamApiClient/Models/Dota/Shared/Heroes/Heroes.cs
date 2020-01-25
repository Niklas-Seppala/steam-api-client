using Newtonsoft.Json;

namespace SteamApiClient.Models
{
    public class Heroes
    {
        [JsonProperty("result")]
        public HeroesContent Content { get; set; }
    }
}
