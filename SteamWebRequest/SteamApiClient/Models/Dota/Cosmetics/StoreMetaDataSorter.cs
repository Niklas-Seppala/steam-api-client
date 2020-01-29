using Newtonsoft.Json;

namespace SteamApiClient.Models.Dota
{
    public class StoreMetaDataSorter
    {
        public ulong Id { get; set; }
        public string Name { get; set; }

        [JsonProperty("data_type")]
        public string DataType { get; set; }

        [JsonProperty("sort_field")]
        public string SortField { get; set; }

        [JsonProperty("sort_reversed")]
        public bool SortReversed { get; set; }

        [JsonProperty("localized_text")]
        public string LocalizedText { get; set; }
    }
}
