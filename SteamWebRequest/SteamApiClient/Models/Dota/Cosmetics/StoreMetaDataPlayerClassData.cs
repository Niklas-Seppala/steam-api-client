using Newtonsoft.Json;

namespace SteamApiClient.Models.Dota
{
    public class StoreMetaDataPlayerClassData
    {
        public ushort Id { get; set; }

        [JsonProperty("base_name")]
        public string BaseName { get; set; }

        [JsonProperty("localized_text")]
        public string LocalizedText { get; set; }
    }
}
