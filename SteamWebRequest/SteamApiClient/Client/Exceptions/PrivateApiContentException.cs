using System;
using System.Runtime.Serialization;

namespace SteamApi
{
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
