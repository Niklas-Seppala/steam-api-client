using Newtonsoft.Json;
using System;

namespace SteamApiClient.Models.Dota
{
    public sealed class AbilityUpgradeEvent
    {
        [JsonConverter(typeof(TimeSpanConverter))]
        public TimeSpan Time { get; set; }

        public uint Ability { get; set; }
        public byte Level { get; set; }
    }
}
