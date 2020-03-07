using System;
using System.Runtime.Serialization;

namespace SteamApi
{
    /// <summary>
    /// Thrown when API response didn't return expected data.
    /// </summary>
    /// <typeparam name="T">Expected response model type</typeparam>
    [Serializable]
    public class EmptyApiResponseException<T> : ApiException
    {
        /// <summary>
        /// Type of the Requested model
        /// </summary>
        public Type ResponseModelType { get; } = typeof(T);

        public EmptyApiResponseException() { }
        public EmptyApiResponseException(string message) : base(message) { }
        public EmptyApiResponseException(string message, Exception inner) : base(message, inner) { }
        protected EmptyApiResponseException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }


    [Serializable]
    public class PrivateApiResponseException : ApiException
    {
        public PrivateApiResponseException() { }
        public PrivateApiResponseException(string message) : base(message) { }
        public PrivateApiResponseException(string message, Exception inner) : base(message, inner) { }
        protected PrivateApiResponseException(
          SerializationInfo info,
          StreamingContext context) : base(info, context) { }
    }
}
