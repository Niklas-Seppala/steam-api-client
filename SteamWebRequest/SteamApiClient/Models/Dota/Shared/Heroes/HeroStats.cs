using Newtonsoft.Json;

namespace SteamApiClient.Models.Dota
{
    public class HeroStats
    {
        [JsonProperty("dname")]
        public string LocalizedName { get; set; }

        [JsonProperty("u")]
        public string Name { get; set; }

        [JsonProperty("pa")]
        public string PrimaryAttribute { get; set; }

        [JsonProperty("attribs")]
        public Attributes Attributes { get; set; }

    }
}
