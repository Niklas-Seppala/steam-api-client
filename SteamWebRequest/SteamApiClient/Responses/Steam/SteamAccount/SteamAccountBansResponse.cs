using System;

namespace SteamApi.Responses.Steam
{
    [Serializable]
    public class SteamAccountBansResponse : ApiResponse
    {
        public AccountBans Contents { get; set; }
    }
}
