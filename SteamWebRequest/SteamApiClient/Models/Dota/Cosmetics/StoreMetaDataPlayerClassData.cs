using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class StoreMetaDataPlayerClassData
    {
        public uint Id { get; set; }
        [JsonProperty("base_name")]
        public string BaseName { get; set; }
        [JsonProperty("localized_text")]
        public string LocalizedText { get; set; }
    }
}
