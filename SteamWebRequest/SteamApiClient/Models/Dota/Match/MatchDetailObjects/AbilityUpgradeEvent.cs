﻿namespace SteamApi.Models.Dota
{
    public sealed class AbilityUpgradeEvent
    {
        public uint Time { get; set; }
        public uint Ability { get; set; }
        public uint Level { get; set; }
    }
}
