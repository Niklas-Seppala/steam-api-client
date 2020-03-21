using System;

namespace SteamApi.Responses.Dota
{
    /// <summary>
    /// API response to store metadata request.
    /// </summary>
    [Serializable]
    public sealed class StoreMetadataResponse : ApiResponse
    {
        /// <summary>
        /// API response contents.
        /// </summary>
        public StoreMetadata Contents { get; set; }
    }
}
