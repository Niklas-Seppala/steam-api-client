using Newtonsoft.Json;

namespace SteamApi.Models.Steam
{
    /// <summary>
    /// Steam product model
    /// </summary>
    public sealed class SteamProduct
    {
        /// <summary>
        /// Id of the steam product
        /// </summary>
        [JsonProperty("appid")]
        public long AppId { get; set; }

        /// <summary>
        /// Name of the steam product
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Unixtimestamp of the last date product was modified
        /// </summary>
        [JsonProperty("last_modified")]
        public ulong LastModified { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("price_change_number")]
        public ulong PriceChangeNumber { get; set; }
    }
}