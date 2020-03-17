using System;

namespace SteamApi.Responses
{
    /// <summary>
    /// Base class for API response models
    /// </summary>
    public class ApiResponse: IApiResponse
    {
        /// <summary>
        /// Was request successful
        /// </summary>
        public bool Successful { get; set; }
 
        /// <summary>
        /// To which URL request was sent.
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// Possible exception that caused failure
        /// </summary>
        public Exception ThrownException { get; set; }
    }
}
