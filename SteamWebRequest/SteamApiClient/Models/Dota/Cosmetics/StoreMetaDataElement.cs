using Newtonsoft.Json;

namespace SteamApiClient.Models.Dota
{
    public class StoreMetaDataElement
    {
        [JsonProperty("localized_text")]
        public string LocalizedText { get; set; }

        public string Name { get; set; }
        public ulong Id { get; set; }
    }
}
