using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public sealed class SteamProducts
    {
        [JsonProperty("response")]
        public Products Content { get; set; }

        public List<Product> Products => this.Content.ProductList;
    }
}


