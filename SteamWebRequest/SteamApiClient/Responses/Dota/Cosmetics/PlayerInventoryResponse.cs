using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response for player inventory request.
    /// </summary>
    [Serializable]
    public sealed class PlayerInventoryResponse : ApiResponse
    {
        /// <summary>
        /// API response contents
        /// </summary>
        public PlayerInventory Contents { get; set; }
    }
}
