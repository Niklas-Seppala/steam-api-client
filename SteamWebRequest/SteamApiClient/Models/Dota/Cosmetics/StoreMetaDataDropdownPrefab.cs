using System;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    [Serializable]
    public sealed class StoreMetaDataDropdownPrefab
    {
        public ulong Id { get; set; }
        public string Name { get; set; }
        public IReadOnlyCollection<StoreMetaDataDropdownPrefabConfig> Config { get; set; }
    }
}
