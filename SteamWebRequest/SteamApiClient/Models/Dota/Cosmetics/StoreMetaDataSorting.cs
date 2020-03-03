using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class StoreMetaDataSorting
    {
        public IReadOnlyList<StoreMetaDataSorter> Sorters { get; set; }
        [JsonProperty("sorting_prefabs")]
        public IReadOnlyList<StoreMetaDataSortingPrefab> SortingPrefabs { get; set; }
    }
}
