using System;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    [Serializable]
    public sealed class StoreMetaDataDropdownData
    {
        public IReadOnlyList<StoreMetaDataDropdown> Dropdowns { get; set; }
        public IReadOnlyList<StoreMetaDataDropdownPrefab> Prefabs { get; set; }
    }
}
