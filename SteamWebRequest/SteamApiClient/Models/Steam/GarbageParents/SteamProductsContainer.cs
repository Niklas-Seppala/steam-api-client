using Newtonsoft.Json;

namespace SteamApiClient.Models
{
    internal class SteamProductsContainer
    {
        [JsonProperty("response")]
        public ProductContainer Content { get; set; }
    }
}


