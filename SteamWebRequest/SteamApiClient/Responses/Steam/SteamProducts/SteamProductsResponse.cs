using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Steam
{
    [Serializable]
    public sealed class SteamProductsResponse : ApiResponse
    {
        public IReadOnlyList<SteamProduct> Contents { get; set; }

        public ulong LastAppId { get; set; }

        public bool MoreResults { get; set; }
    }
}