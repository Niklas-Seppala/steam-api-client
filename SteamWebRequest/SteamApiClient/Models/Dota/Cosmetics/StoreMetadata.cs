using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    [Serializable]
    public sealed class StoreMetadata
    {
        public IReadOnlyList<StoreMetaDataTab> Tabs { get; set; }

        public IReadOnlyList<StoreMetaDataFilter> Filters { get; set; }

        public StoreMetaDataSorting Sorting { get; set; }

        [JsonProperty("dropdown_data")]
        public StoreMetaDataDropdownData DropDownData { get; set; }

        [JsonProperty("player_class_data")]
        public IReadOnlyList<StoreMetaDataPlayerClassData> PlayerClassData { get; set; }

        [JsonProperty("home_page_data")]
        public IReadOnlyDictionary<string, ulong> HomePageData { get; set; }
    }
}
