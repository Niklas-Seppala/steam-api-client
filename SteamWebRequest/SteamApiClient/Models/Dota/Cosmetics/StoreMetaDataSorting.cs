using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    public class StoreMetaDataSorting
    {
        public IReadOnlyCollection<StoreMetaDataSorter> Sorters { get; set; }

        [JsonProperty("sorting_prefabs")]
        public IReadOnlyCollection<StoreMetaDataSortingPrefab> SortingPrefabs { get; set; }
    }
}
