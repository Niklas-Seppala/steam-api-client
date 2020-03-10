using System;

namespace SteamApi.Models.CsGo
{
    [Serializable]
    public sealed class PerfectWorldStatus
    {
        public string Latency { get; set; }
        public string Availability { get; set; }
    }
}