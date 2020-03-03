using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class StoreMetaDataDropdownData
    {
        public IReadOnlyList<StoreMetaDataDropdown> Dropdowns { get; set; }
        public IReadOnlyList<StoreMetaDataDropdownPrefab> Prefabs { get; set; }
    }
}
