using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    internal class HeroStatsContainer
    {
        [JsonProperty("herodata")]
        public IReadOnlyDictionary<string, HeroStats> HeroStats { get; set; }
    }

}
