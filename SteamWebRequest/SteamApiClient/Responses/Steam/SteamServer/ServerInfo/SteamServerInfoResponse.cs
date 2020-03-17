using System;

namespace SteamApi.Responses.Steam
{
    [Serializable]
    public sealed class SteamServerInfoResponse : ApiResponse
    {
        public SteamServerInfo Contents { get; set; }
    }
}
