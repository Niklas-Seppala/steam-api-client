using Newtonsoft.Json;

namespace SteamWebRequest.Models
{
    public class Hero
    {
        public int Id { get; set; }

        [JsonProperty("Localized_name")]
        public string LocalizedName { get; set; }

        public string Name { get; set; }
    }

}
