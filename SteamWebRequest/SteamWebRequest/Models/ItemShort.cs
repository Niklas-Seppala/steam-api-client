using Newtonsoft.Json;

namespace SteamWebRequest
{
    public sealed class ItemShort
    {
        [JsonProperty("Localized_name")]
        public string LocalizedName { get; set; }

        public int Id { get; set; }
        public int Cost { get; set; }
        public string Name { get; set; }
    }
}
