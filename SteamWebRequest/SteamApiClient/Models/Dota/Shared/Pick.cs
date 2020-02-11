using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class Pick
    {
        [JsonProperty("hero_id")]
        public ushort HeroId { get; set; }
    }
}
