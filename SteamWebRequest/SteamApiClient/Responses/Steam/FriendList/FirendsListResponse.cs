using SteamApi.Responses.Steam.ParentResponses;
using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Steam
{
    [Serializable]
    public class FirendsListResponse : ApiResponse
    {
        public IReadOnlyList<Friend> Contents { get; set; }
    }
}
