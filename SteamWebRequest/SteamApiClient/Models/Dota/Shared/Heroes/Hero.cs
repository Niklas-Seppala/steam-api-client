using Newtonsoft.Json;

namespace SteamApi.Models.Dota
{
    /// <summary>
    /// Dota 2 Hero model
    /// </summary>
    public class Hero
    {
        /// <summary>
        /// Localized Hero name
        /// </summary>
        [JsonProperty("Localized_name")]
        public string LocalizedName { get; set; }

        /// <summary>
        /// Hero id
        /// </summary>
        public uint Id { get; set; }

        /// <summary>
        /// Hero name
        /// </summary>
        public string Name { get; set; }
    }
}
