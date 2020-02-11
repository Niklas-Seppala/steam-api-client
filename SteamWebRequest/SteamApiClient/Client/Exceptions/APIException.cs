using System;
using System.Runtime.Serialization;

namespace SteamApi
{
    [Serializable]
    public class APIException : Exception
    {
        public string Content { get; set; }
        public int StatusCode { get; set; }
        public string URL { get; set; }

        public APIException() { }
        public APIException(string message) : base(message) { }
        public APIException(string message, Exception inner) : base(message, inner) { }
        protected APIException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}
