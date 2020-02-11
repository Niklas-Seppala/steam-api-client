using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApi.Models.Dota
{
    internal class Abilities
    {
        [JsonProperty("abilitydata")]
        public IReadOnlyDictionary<string, Ability> AbilityDict { get; set; }
    }
}
