using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    public class StoreMetadata
    {
        public IReadOnlyCollection<StoreMetaDataTab> Tabs { get; set; }
        public IReadOnlyCollection<StoreMetaDataFilter> Filters { get; set; }
        public StoreMetaDataSorting Sorting { get; set; }
        public StoreMetaDataDropdownData DropDownData { get; set; }

        [JsonProperty("player_class_data")]
        public IReadOnlyCollection<StoreMetaDataPlayerClassData> PlayerClassData { get; set; }

        [JsonProperty("home_page_data")]
        public IReadOnlyDictionary<string, ulong> HomePageData { get; set; }
    }
}
