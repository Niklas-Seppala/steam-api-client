using System;
using System.Runtime.Serialization;

namespace SteamApi
{
    /// <summary>
    /// Thrown when API response resource was not found
    /// </summary>
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
