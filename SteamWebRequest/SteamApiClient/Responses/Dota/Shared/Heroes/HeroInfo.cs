using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// Hero info model
    /// </summary>
    [Serializable]
    public sealed class HeroInfo
    {
        /// <summary>
        /// Hero name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Hero Lore
        /// </summary>
        public string Bio { get; set; }

        /// <summary>
        /// Attack type (melee/ranged)
        /// </summary>
        [JsonProperty("atk_l")]
        public string AttackType { get; set; }

        /// <summary>
        /// List of hero roles
        /// </summary>
        [JsonProperty("roles_l")]
        public IReadOnlyList<string> Roles { get; set; }
    }
}
