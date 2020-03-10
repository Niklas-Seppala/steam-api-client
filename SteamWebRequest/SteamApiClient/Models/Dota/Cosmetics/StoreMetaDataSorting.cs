using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    [Serializable]
    public sealed class StoreMetaDataSorting
    {
        public IReadOnlyList<StoreMetaDataSorter> Sorters { get; set; }
        [JsonProperty("sorting_prefabs")]
        public IReadOnlyList<StoreMetaDataSortingPrefab> SortingPrefabs { get; set; }
    }
}
