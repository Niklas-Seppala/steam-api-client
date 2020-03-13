using System;
using System.Runtime.Serialization;

namespace SteamApi
{
    /// <summary>
    /// Thrown when the Steam user's private information was requested.
    /// </summary>
    [Serializable]
    public class ApiPrivateContentException : ApiException
    {
        public ApiPrivateContentException() { }
        public ApiPrivateContentException(string message) : base(message) { }
        public ApiPrivateContentException(string message, Exception inner) : base(message, inner) { }
        protected ApiPrivateContentException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
