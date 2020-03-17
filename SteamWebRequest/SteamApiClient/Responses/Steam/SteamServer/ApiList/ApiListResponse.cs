using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Steam
{
    [Serializable]
    public sealed class ApiListResponse : ApiResponse
    {
        public IReadOnlyList<ApiInterface> Contents { get; set; }
    }
}
