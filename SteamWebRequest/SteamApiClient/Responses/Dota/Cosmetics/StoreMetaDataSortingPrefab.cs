using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    [Serializable]
    public sealed class StoreMetaDataSortingPrefab
    {
        public ulong Id { get; set; }
        public string Name { get; set; }

        [JsonProperty("url_history_param_name")]
        public string UrlHistoryParamName { get; set; }

        [JsonProperty("sorter_ids")]
        public IReadOnlyCollection<IReadOnlyDictionary<string, ulong>> SorterIds { get; set; }
    }
}
