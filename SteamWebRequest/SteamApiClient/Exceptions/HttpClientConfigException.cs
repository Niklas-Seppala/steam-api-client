using System;
using System.Runtime.Serialization;

namespace SteamApiClient
{
    [Serializable]
    public class HttpClientConfigException : Exception
    {
        private static string baseMessage = "Client configuration failed.\n";
        public string OptionalMessage { get; set; }

        public HttpClientConfigException() : base(baseMessage)
        { }
        public HttpClientConfigException(Exception inner) : base(baseMessage, inner)
        { }
        protected HttpClientConfigException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        { }
    }
}
