using System;

namespace SteamApi.Models.CsGo
{
    /// <summary>
    /// CS GO server status service information model
    /// </summary>
    [Serializable]
    public sealed class CsGoServerServices
    {
        /// <summary>
        /// 
        /// </summary>
        public string SessionsLogon { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SteamCommunity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string IEconItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Leaderboards { get; set; }
    }
}