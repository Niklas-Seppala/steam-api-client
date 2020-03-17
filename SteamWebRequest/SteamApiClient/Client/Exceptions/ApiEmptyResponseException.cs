using System;
using System.Runtime.Serialization;

namespace SteamApi
{
    /// <summary>
    /// Thrown when API response didn't return expected data.
    /// </summary>
    [Serializable]
    public class ApiEmptyResultException : ApiException
    {
        public ApiEmptyResultException() { }
        public ApiEmptyResultException(string message) : base(message) { }
        public ApiEmptyResultException(string message, Exception inner) : base(message, inner) { }
        protected ApiEmptyResultException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
