using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class DotaCosmeticRarity
    {
        public string Name { get; set; }
        public uint Id { get; set; }
        public uint Order { get; set; }
        public string Color { get; set; }

        [JsonProperty("localized_name")]
        public string LocalizedName { get; set; }
    }
}
