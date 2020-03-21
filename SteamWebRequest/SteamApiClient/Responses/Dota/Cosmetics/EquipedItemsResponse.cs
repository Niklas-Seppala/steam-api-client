using System;
using System.Collections.Generic;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response for equiped items request.
    /// </summary>
    [Serializable]
    public sealed class EquipedItemsResponse : ApiResponse
    {
        /// <summary>
        /// API response contents
        /// </summary>
        public IReadOnlyList<EquipedItem> Contents { get; set; }
    }
}
