using Newtonsoft.Json;

namespace SteamApiClient.Models.Dota
{
    public class Pick
    {
        [JsonProperty("hero_id")]
        public ushort HeroId { get; set; }
    }
}
