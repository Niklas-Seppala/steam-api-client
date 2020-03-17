using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Steam
{
    [Serializable]
    public class MultipleAccountBansResponse : ApiResponse
    {
        public IReadOnlyList<AccountBans> Contents { get; set; }
    }
}
