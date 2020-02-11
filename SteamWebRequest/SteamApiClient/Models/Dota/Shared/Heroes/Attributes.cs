using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    public class Attributes
    {
        [JsonProperty("str")]
        public Attribute Strength { get; set; }

        [JsonProperty("int")]
        public Attribute Intelligence { get; set; }

        [JsonProperty("agi")]
        public Attribute Agility { get; set; }

        [JsonProperty("ms")]
        public ushort MovementSpeed { get; set; }

        [JsonProperty("armor")]
        public float Armor { get; set; }

        [JsonProperty("dmg")]
        public Damage Damage { get; set; }
    }
}
