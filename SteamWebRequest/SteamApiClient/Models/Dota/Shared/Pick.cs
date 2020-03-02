using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class Pick
    {
        [JsonProperty("hero_id")]
        public uint HeroId { get; set; }
    }
}
