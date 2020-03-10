using Newtonsoft.Json;
using System;

namespace SteamApi.Models.Dota
{
    [Serializable]
    public sealed class StoreMetaDataDropdown
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        [JsonProperty("label_text")]
        public string LabelText { get; set; }

        [JsonProperty("url_history_param_name")]
        public string UrlHistoryParamName { get; set; }
    }
}
