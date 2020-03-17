using Newtonsoft.Json;
using System;

namespace SteamApi.Responses.Dota
{
    [Serializable]
    public sealed class StoreMetaDataElement
    {
        [JsonProperty("localized_text")]
        public string LocalizedText { get; set; }
        public string Name { get; set; }
        public ulong Id { get; set; }
    }
}
