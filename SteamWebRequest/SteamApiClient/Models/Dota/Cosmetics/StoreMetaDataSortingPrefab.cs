using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    public class StoreMetaDataSortingPrefab
    {
        public ulong Id { get; set; }
        public string Name { get; set; }

        [JsonProperty("url_history_param_name")]
        public string UrlHistoryParamName { get; set; }

        [JsonProperty("sorter_ids")]
        public IReadOnlyCollection<IReadOnlyDictionary<string, ulong>> SorterIds { get; set; }
    }
}
