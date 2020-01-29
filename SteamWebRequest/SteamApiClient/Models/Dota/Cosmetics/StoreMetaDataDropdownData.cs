using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    public class StoreMetaDataDropdownData
    {
        public IReadOnlyCollection<StoreMetaDataDropdown> Dropdowns { get; set; }
        public IReadOnlyCollection<StoreMetaDataDropdownPrefab> Prefabs { get; set; }
    }
}
