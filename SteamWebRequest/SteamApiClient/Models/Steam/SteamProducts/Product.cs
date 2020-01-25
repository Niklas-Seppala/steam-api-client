using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace SteamApiClient.Models
{
    public sealed class Product
    {
        [JsonProperty("appid")]
        public long Id { get; set; }

        public string Name { get; set; }

        [JsonConverter(typeof(UnixDateTimeConverter))]
        [JsonProperty("last_modified")]
        public DateTime LastModified { get; set; }

        [JsonProperty("price_change_number")]
        public ulong PriceChangeNumber { get; set; }
    }
}


