using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models
{
    internal class ProductContainer
    {
        [JsonProperty("apps")]
        public List<SteamProduct> ProductList { get; set; }

        [JsonProperty("have_more_results")]
        public bool MoreResults { get; set; }

        [JsonProperty("last_appid")]
        public ulong LastId { get; set; }
    }
}


