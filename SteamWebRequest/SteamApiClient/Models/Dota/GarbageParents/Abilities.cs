using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models.Dota
{
    internal class Abilities
    {
        [JsonProperty("abilitydata")]
        public IReadOnlyDictionary<string, Ability> AbilityDict { get; set; }
    }
}
