using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    public class StoreMetaDataFilter
    {
        public ulong Id { get; set; }
        public string Name { get; set; }

        [JsonProperty("url_history_param_name")]
        public string UrlHistoryParamName { get; set; }

        [JsonProperty("all_element")]
        public IReadOnlyDictionary<string, string> AllElement { get; set; }

        public IReadOnlyCollection<StoreMetaDataElement> Elements { get; set; }

        public int Count { get; set; }
    }
}
