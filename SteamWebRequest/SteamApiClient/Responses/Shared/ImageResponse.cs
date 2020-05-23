using System;
using System.Collections.Generic;
using System.Text;

namespace SteamApi.Responses
{
    /// <summary>
    /// Response to hero image request.
    /// </summary>
    [Serializable]
    public class ImageResponse : ApiResponse
    {
        /// <summary>
        /// Image bytes.
        /// </summary>
        public byte[] Contents { get; set; }
    }
}
