using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System;

namespace SteamWebRequest.Models
{
    public sealed class SteamProducts
    {
        [JsonProperty("response")]
        public Products Content { get; set; }

        public List<Product> Products { get => this.Content.ProductList; }
    }

    public sealed class Products
    {
        [JsonProperty("apps")]
        public List<Product> ProductList { get; set; }

        [JsonProperty("have_more_results")]
        public bool MoreResults { get; set; }

        [JsonProperty("last_appid")]
        public ulong LastId { get; set; }
    }

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


