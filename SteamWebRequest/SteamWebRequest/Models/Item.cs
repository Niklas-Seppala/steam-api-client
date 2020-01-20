using Newtonsoft.Json;

namespace SteamWebRequest.Models
{
    public sealed class Item
    {
        public int Id { get; set; }

        public int Cost { get; set; }

        [JsonProperty("Localized_name")]
        public string LocalizedName { get; set; }

        public string Name { get; set; }
    }

}
