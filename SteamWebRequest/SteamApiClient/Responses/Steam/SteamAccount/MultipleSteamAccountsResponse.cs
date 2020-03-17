using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Steam
{
    [Serializable]
    public class MultipleSteamAccountsResponse : ApiResponse
    {
        public IReadOnlyList<SteamAccount> Contents { get; set; }
    }
}
