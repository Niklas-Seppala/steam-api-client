using System;

namespace SteamApi.Models.CsGo
{
    /// <summary>
    /// CS GO datacenter model
    /// </summary>
    [Serializable]
    public sealed class CsGoDataCenter
    {
        /// <summary>
        /// Datacenter capacity
        /// </summary>
        public string Capacity { get; set; }

        /// <summary>
        /// Datacenter load
        /// </summary>
        public string Load { get; set; }
    }
}