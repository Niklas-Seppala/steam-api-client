using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public sealed class ItemShort
    {
        [JsonProperty("Localized_name")]
        public string LocalizedName { get; set; }

        public uint Id { get; set; }
        public uint Cost { get; set; }
        public string Name { get; set; }
    }
}
