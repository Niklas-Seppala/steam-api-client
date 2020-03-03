using System;
using System.Runtime.Serialization;

namespace SteamApi
{
    /// <summary>
    /// Thrown when ApiCLient setup has failed.
    /// Most propable cause is invalid API authentication key
    /// </summary>
    [Serializable]
    public class ApiClientSetupException : Exception
    {
        public ApiClientSetupException()
        { }
        public ApiClientSetupException(string message = "API Client setup failed. Check inner exception for cause.")
            : base(message) { }
        public ApiClientSetupException(string message, Exception inner) : base(message, inner)
        { }
        protected ApiClientSetupException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
