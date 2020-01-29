using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    public class HeroInfo
    {
        public string Name { get; set; }
        public string Bio { get; set; }

        [JsonProperty("atk_l")]
        public string AttackType { get; set; }

        [JsonProperty("roles_l")]
        public IReadOnlyCollection<string> Roles { get; set; }
    }
}
