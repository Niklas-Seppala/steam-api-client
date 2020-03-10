using System;

namespace SteamApi.Models.CsGo
{
    [Serializable]
    public sealed class CsGoPerfectWorld
    {
        public PerfectWorldStatus Logon { get; set; }
        public PerfectWorldStatus Purchase { get; set; }
    }
}