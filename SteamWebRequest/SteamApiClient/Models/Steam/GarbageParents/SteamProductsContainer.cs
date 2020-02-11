using Newtonsoft.Json;

namespace SteamApi.Models
{
    internal class SteamProductsContainer
    {
        [JsonProperty("response")]
        public ProductContainer Content { get; set; }
    }
}


