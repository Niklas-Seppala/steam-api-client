using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public sealed class Products
    {
        [JsonProperty("apps")]
        public List<Product> ProductList { get; set; }

        [JsonProperty("have_more_results")]
        public bool MoreResults { get; set; }

        [JsonProperty("last_appid")]
        public ulong LastId { get; set; }
    }
}


