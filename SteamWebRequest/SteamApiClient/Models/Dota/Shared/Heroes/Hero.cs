using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class Hero
    {
        [JsonProperty("Localized_name")]
        public string LocalizedName { get; set; }
        public uint Id { get; set; }
        public string Name { get; set; }
    }
}
