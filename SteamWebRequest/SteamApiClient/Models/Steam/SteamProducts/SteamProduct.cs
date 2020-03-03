using Newtonsoft.Json;

namespace SteamApi.Models
{
    public class SteamProduct
    {
        [JsonProperty("appid")]
        public long Id { get; set; }

        public string Name { get; set; }

        [JsonProperty("last_modified")]
        public ulong LastModified { get; set; }


        [JsonProperty("price_change_number")]
        public ulong PriceChangeNumber { get; set; }
    }
}


