using System;
using System.Runtime.Serialization;

namespace SteamApi
{
    [Serializable]
    public class ApiResourceNotFoundException : ApiException
    {
        public ApiResourceNotFoundException() { }
        public ApiResourceNotFoundException(string message) : base(message) { }
        public ApiResourceNotFoundException(string message, Exception inner) : base(message, inner) { }
        protected ApiResourceNotFoundException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
