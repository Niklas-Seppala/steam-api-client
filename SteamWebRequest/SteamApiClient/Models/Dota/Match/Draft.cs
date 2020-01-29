using Newtonsoft.Json;

namespace SteamApiClient.Models.Dota
{
    public class Draft
    {
        [JsonProperty("is_pick")]
        public bool IsPick { get; set; }

        [JsonProperty("hero_id")]
        public ushort HeroId { get; set; }

        public uint Team { get; set; }

        public byte Order { get; set; }
    }
}
