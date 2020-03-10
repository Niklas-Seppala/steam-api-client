using Newtonsoft.Json;
using System;

namespace SteamApi.Models.Dota
{
    [Serializable]
    public sealed class DotaCosmeticRarity
    {
        public string Name { get; set; }
        public uint Id { get; set; }
        public uint Order { get; set; }
        public string Color { get; set; }
        [JsonProperty("localized_name")]
        public string LocalizedName { get; set; }
    }
}
