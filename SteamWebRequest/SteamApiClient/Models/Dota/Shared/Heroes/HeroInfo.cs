using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public class HeroInfo
    {
        public string Name { get; set; }
        public string Bio { get; set; }

        [JsonProperty("atk_l")]
        public string AttackType { get; set; }

        [JsonProperty("roles_l")]
        public List<string> Roles { get; set; }
    }
}
