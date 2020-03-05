using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Hero ability model
    /// </summary>
    public class Ability
    {
        /// <summary>
        /// Localized name
        /// </summary>
        [JsonProperty("dname")]
        public string LocalizedName { get; set; }

        /// <summary>
        /// Affects description
        /// </summary>
        public string Affects { get; set; }

        /// <summary>
        /// Ability description
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Item damage
        /// </summary>
        [JsonProperty("dmg")]
        public string Damage { get; set; }

        /// <summary>
        /// Item Attributes
        /// </summary>
        [JsonProperty("attrib")]
        public string Attribute { get; set; }

        /// <summary>
        /// Item lore text
        /// </summary>
        public string Lore { get; set; }

        /// <summary>
        /// Item hurl
        /// </summary>
        public string Hurl { get; set; }
    }
}
