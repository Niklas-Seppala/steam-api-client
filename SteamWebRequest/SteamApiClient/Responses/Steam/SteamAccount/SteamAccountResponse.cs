using System;

namespace SteamApi.Responses.Steam
{
    [Serializable]
    public class SteamAccountResponse : ApiResponse
    {
        public SteamAccount Contents { get; set; }
    }
}
