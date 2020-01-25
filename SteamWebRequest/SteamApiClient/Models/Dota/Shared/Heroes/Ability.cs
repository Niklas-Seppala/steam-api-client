using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public class Ability
    {
        [JsonProperty("dname")]
        public string LocalizedName { get; set; }

        public string Affects { get; set; }

        public string Notes { get; set; }

        [JsonProperty("dmg")]
        public string Damage { get; set; }

        [JsonProperty("attrib")]
        public string Attribute { get; set; }

        public string Lore { get; set; }

        public string Hurl { get; set; }
    }

    public class Abilities
    {
        [JsonProperty("abilitydata")]
        public Dictionary<string, Ability> AbilityDict { get; set; }
    }
}
