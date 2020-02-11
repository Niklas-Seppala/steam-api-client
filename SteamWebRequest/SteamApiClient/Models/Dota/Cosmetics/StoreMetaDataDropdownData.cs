using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    public class StoreMetaDataDropdownData
    {
        public IReadOnlyCollection<StoreMetaDataDropdown> Dropdowns { get; set; }
        public IReadOnlyCollection<StoreMetaDataDropdownPrefab> Prefabs { get; set; }
    }
}
