using System;
using System.Runtime.Serialization;

namespace SteamApi
{
    /// <summary>
    /// Thrown when API response didn't return expected data.
    /// </summary>
    /// <typeparam name="T">Expected response model type</typeparam>
    [Serializable]
    public class EmptyApiResultException<T> : ApiException
    {
        /// <summary>
        /// Type of the Requested model
        /// </summary>
        public Type ResponseModelType { get; } = typeof(T);

        public EmptyApiResultException() { }
        public EmptyApiResultException(string message) : base(message) { }
        public EmptyApiResultException(string message, Exception inner) : base(message, inner) { }
        protected EmptyApiResultException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
