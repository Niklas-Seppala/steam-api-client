using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    [Serializable]
    public sealed class StoreMetaDataTab
    {
        public string Label { get; set; }
        public string Id { get; set; }
        [JsonProperty("parent_id")]
        public ulong ParentId { get; set; }
        public bool Default { get; set; }
        public bool Home { get; set; }
        [JsonProperty("dropdown_prefab_id")]
        public ulong DropDownPrefabId { get; set; }
        [JsonProperty("tab_image_override_name")]
        public string TabImageOverrideName { get; set; }
        public IReadOnlyList<IReadOnlyDictionary<string, string>> Children { get; set; }
    }
}
