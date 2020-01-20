using Newtonsoft.Json;

namespace SteamWebRequest.Models
{
    public sealed class Item
    {
        [JsonProperty("Localized_name")]
        public string LocalizedName { get; set; }

        public int Id { get; set; }
        public int Cost { get; set; }
        public string Name { get; set; }
    }
}
