using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    internal class HeroStatsContainer
    {
        [JsonProperty("herodata")]
        public IReadOnlyDictionary<string, HeroStats> HeroStats { get; set; }
    }

}
