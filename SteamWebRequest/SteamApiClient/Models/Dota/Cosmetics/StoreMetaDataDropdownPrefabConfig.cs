using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class StoreMetaDataDropdownPrefabConfig
    {
        [JsonProperty("dropdown_id")]
        public ulong DropdownId { get; set; }

        public string Name { get; set; }
        public bool Enabled { get; set; }

        [JsonProperty("defaul_selection_id")]
        public ulong DefaultSelectionId { get; set; }
    }
}
