using System;
using System.Runtime.Serialization;

namespace SteamApi
{
    /// <summary>
    /// Thrown when API call method fails to deliver
    /// excpected result.
    /// </summary>
    [Serializable]
    public class ApiException : Exception
    {
        /// <summary>
        /// Steam API status code
        /// </summary>
        public uint ApiStatusCode { get; set; }

        /// <summary>
        /// Server response status code
        /// </summary>
        public int HttpStatusCode { get; set; }

        public ApiException() { }
        public ApiException(string message) : base(message) { }
        public ApiException(string message, Exception inner) : base(message, inner) { }
        protected ApiException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
