using System;
using System.Runtime.Serialization;

namespace SteamApi
{
    /// <summary>
    /// Thrown when the Steam user's private information was requested.
    /// </summary>
    [Serializable]
    public class PrivateApiContentException : ApiException
    {
        public PrivateApiContentException() { }
        public PrivateApiContentException(string message) : base(message) { }
        public PrivateApiContentException(string message, Exception inner) : base(message, inner) { }
        protected PrivateApiContentException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
