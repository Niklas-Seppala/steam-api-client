using Newtonsoft.Json;
using System;

namespace SteamApi.Models.Dota
{
    public sealed class AbilityUpgradeEvent
    {
        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan Time { get; set; }

        public uint Ability { get; set; }
        public uint Level { get; set; }
    }
}
