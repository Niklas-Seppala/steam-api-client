using Newtonsoft.Json;
using System;

namespace SteamApi.Models.Dota
{
    [Serializable]
    public sealed class StoreMetaDataPlayerClassData
    {
        public uint Id { get; set; }
        [JsonProperty("base_name")]
        public string BaseName { get; set; }
        [JsonProperty("localized_text")]
        public string LocalizedText { get; set; }
    }
}
