using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class Attribute
    {
        [JsonProperty("b")]
        public uint Base { get; set; }

        [JsonProperty("g")]
        public float Gain { get; set; }
    }
}
