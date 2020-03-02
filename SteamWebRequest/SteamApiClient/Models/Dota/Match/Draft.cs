using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class Draft
    {
        [JsonProperty("is_pick")]
        public bool IsPick { get; set; }

        [JsonProperty("hero_id")]
        public uint HeroId { get; set; }

        public uint Team { get; set; }

        public uint Order { get; set; }
    }
}
