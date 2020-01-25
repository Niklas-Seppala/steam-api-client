using Newtonsoft.Json;
using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public class HeroStats
    {
        [JsonProperty("dname")]
        public string LocalizedName { get; set; }

        [JsonProperty("u")]
        public string Name { get; set; }

        [JsonProperty("pa")]
        public string PrimaryAttribute { get; set; }

        [JsonProperty("attribs")]
        public Attributes Attributes { get; set; }

    }

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

    public class Damage
    {
        public ushort Min { get; set; }
        public ushort Max { get; set; }
    }

    public class Attribute
    {
        [JsonProperty("b")]
        public int Base { get; set; }

        [JsonProperty("g")]
        public float Gain { get; set; }
    }

    internal class HeroStatsContainer
    {
        [JsonProperty("herodata")]
        public Dictionary<string, HeroStats> HeroStats { get; set; }
    }

}
