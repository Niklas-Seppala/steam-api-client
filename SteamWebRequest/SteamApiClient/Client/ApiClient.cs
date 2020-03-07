using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CToken = System.Threading.CancellationToken;
using SteamApi.Utility;

namespace SteamApi
{
    /// <summary>
    /// Abstract base class that more specified API client
    /// classes are derived from. Provides simple async methods
    /// to call APIs.
    /// 
    /// Main components:
    /// 
    ///     ApiKey - A shared API authentication key in this parent class is accessed
    ///              in most of the API requests the child classes make.
    ///              read more: https://steamcommunity.com/dev
    ///              
    ///     Client - A single shared HttpClient object that processes every
    ///              API request the child classes make.
    ///              Exists through whole application lifetime.
    /// </summary>
    public abstract class ApiClient
    {
        /// <summary>
        /// Api authentication key. This key is only assigned
        /// to this class. It's children have access to it.
        /// </summary>
        protected static string ApiKey { get; private set; }

        /// <summary>
        /// Single static HttpClient. Child classes make their
        /// requests using this instance. Must be readonly.
        /// </summary>
        protected static HttpClient Client { get; } = new HttpClient();

        /// <summary>
        /// UrlBuilder that is responsible for creating urls for 
        /// API method calls.
        /// </summary>
        protected private UrlBuilder UrlBuilder { get; }

        /// <summary>
        /// Test url to some Steam API method. Used in constructor
        /// if user wants to test their key before continuing.
        /// </summary>
        protected abstract string TestUrl { get; }

        /// <summary>
        /// Sets API authentication key to static field
        /// in base API client class.
        /// </summary>
        /// <param name="apiKey">Api auth key</param>
        public static void SetApiKey(string devKey)
        {
            ApiKey = devKey;
        }

        /// <summary>
        /// Base constructor. Sets default schema for urls
        /// and tests ApiKey if you want.
        /// </summary>
        /// <param name="testConnection">default: false</param>
        /// <param name="schema">default: "https"</param>
        protected ApiClient(bool testConnection, string schema)
        {
            UrlBuilder = new UrlBuilder().SetSchema(schema);
            if (testConnection)
                TestRequest().Wait();
        }

        /// <summary>
        /// Tests connection to server and Api authentication
        /// key. In case of failure throws exceptions normally.
        /// </summary>
        protected async Task TestRequest()
        {
            using (HttpResponseMessage response = await Client.GetAsync(TestUrl))
                await HandleResults<object>(response, (r) => null);
        }

        /// <summary>
        /// Servers response indicates that something went wrong.
        /// Translates Http error code to exception and throws it.
        /// </summary>
        /// <param name="code">Http response status code</param>
        private void ThrowFailedRequestException(HttpResponseMessage resp)
        {
            int code = (int)resp.StatusCode;
            switch (resp.StatusCode)
            {
                case HttpStatusCode.Unauthorized:
                    throw new PrivateApiContentException($"Response status code: {code}. Unauthorized request")
                    {
                        URL = resp.RequestMessage.RequestUri.ToString(),
                        StatusCode = code
                    };
                case HttpStatusCode.BadGateway:
                    throw new HttpRequestException($"Response status code: {code}. Bad Gateway");
                case HttpStatusCode.BadRequest:
                    throw new HttpRequestException($"Response status code: {code}. Bad Request");
                case HttpStatusCode.Forbidden:
                    throw new HttpRequestException($"Response status code: {code}. Developer key is invalid.");
                case HttpStatusCode.GatewayTimeout:
                    throw new HttpRequestException($"Response status code: {code}. Gateway Timeout");
                case HttpStatusCode.HttpVersionNotSupported:
                    throw new HttpRequestException($"Response status code: {code}. Http Version Not Supported");
                case HttpStatusCode.InternalServerError:
                    throw new HttpRequestException($"Response status code: {code}. Internal Server Error");
                case HttpStatusCode.MethodNotAllowed:
                    throw new HttpRequestException($"Response status code: {code}. Method Not Allowed");
                case HttpStatusCode.Gone:
                    throw new ApiResourceNotFoundException($"Response status code: {code}. Resource No Longer Available")
                    {
                        URL = resp.RequestMessage.RequestUri.ToString(),
                        StatusCode = code
                    };
                case HttpStatusCode.Moved:
                    throw new ApiResourceNotFoundException($"Response status code: {code}. Content Moved")
                    {
                        URL = resp.RequestMessage.RequestUri.ToString(),
                        StatusCode = code
                    };
                case HttpStatusCode.NotFound:
                    throw new ApiResourceNotFoundException($"Response status code: {code}. Not Found")
                    {
                        URL = resp.RequestMessage.RequestUri.ToString(),
                        StatusCode = code
                    };
                case HttpStatusCode.NotImplemented:
                    throw new HttpRequestException($"Response status code: {code}. Not Implemented");
                case HttpStatusCode.RequestTimeout:
                    throw new HttpRequestException($"Response status code: {code}. Request Timeout");
                case HttpStatusCode.ServiceUnavailable:
                    throw new HttpRequestException($"Response status code: {code}. Service Unavailable");
                default:
                    throw new HttpRequestException($"Response status code: {code}. Something went wrong with request.");
            }
        }

        /// <summary>
        /// Sends get request to url. Returns response as byte
        /// array. Request can be cancelled by providing cancellation
        /// token.
        /// </summary>
        /// <param name="url">API request url</param>
        /// <param name="cToken">cancellation token</param>
        /// <returns>API response as bytes</returns>
        protected async Task<byte[]> GetBytesAsync(string url = "", CToken cToken = default)
        {
            using (var request = CreateRequest(HttpMethod.Get, url))
            using (var response = await SendRequestAsync(request, cToken).ConfigureAwait(false))
                return await HandleResults(response, async (r) => await r.Content.ReadAsByteArrayAsync());
        }

        /// <summary>
        /// Sends get request to url. Returns response as memory stream.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="url">API request url</param>
        /// <param name="cToken">cancellation token</param>
        /// <returns>API response as memory stream</returns>
        protected async Task<Stream> GetStreamAsync(string url = "", CToken cToken = default)
        {
            using (var request = CreateRequest(HttpMethod.Get, url))
            using (var response = await SendRequestAsync(request, cToken).ConfigureAwait(false))
                return await HandleResults(response, async (r) => await r.Content.ReadAsStreamAsync());
        }

        /// <summary>
        /// Sends get request to url. Returns response as a string.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="url">API request url</param>
        /// <param name="cToken">cancellation token</param>
        /// <returns>API response as string</returns>
        protected async Task<string> GetStringAsync(string url = "", CToken cToken = default)
        {
            using (var request = CreateRequest(HttpMethod.Get, url))
            using (var response = await SendRequestAsync(request, cToken).ConfigureAwait(false))
                return await HandleResults(response, async (r) => await r.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Sends GET request to API and deserializes
        /// response to model object. Request can be cancelled
        /// by providing cancellation token.
        /// </summary>
        /// <typeparam name="T">Type of the model</typeparam>
        /// <param name="url">API request url</param>
        /// <param name="cToken">cancellation token</param>
        /// <returns>API response deserialized to model</returns>
        protected async Task<T> GetModelAsync<T>(string url = "", CToken cToken = default)
        {
            using (var request = CreateRequest(HttpMethod.Get, url))
            using (var response = await SendRequestAsync(request, cToken).ConfigureAwait(false))
            {
                return await HandleResults(response, async (resp) =>
                {
                    using (var contentStream = await resp.Content.ReadAsStreamAsync())
                        return DeserializeJsonStream<T>(contentStream);
                });
            }
        }

        /// <summary>
        /// Deserializes json stream to defined model
        /// object.
        /// </summary>
        /// <returns>Model object</returns>
        private T DeserializeJsonStream<T>(Stream stream)
        {
            if (stream.CanRead)
            {
                using (var streamReader = new StreamReader(stream))
                using (var JsonReader = new JsonTextReader(streamReader))
                    return new JsonSerializer().Deserialize<T>(JsonReader);
            }
            else
            {
                if (stream == null) throw new ArgumentNullException("stream is null");
                else throw new ArgumentException("stream is writeonly");
            }
        }

        /// <summary>
        /// Handles api response based on HttpResponseMessage
        /// status code. Throws errors if needed. provide success function.
        /// </summary>
        /// <typeparam name="T">request result type</typeparam>
        /// <param name="response">api response</param>
        /// <param name="successAction">success function</param>
        private async Task<T> HandleResults<T>(HttpResponseMessage response, Func<HttpResponseMessage, Task<T>> successAction)
        {
            if (response.IsSuccessStatusCode) return await successAction.Invoke(response).ConfigureAwait(false);
            else ThrowFailedRequestException(response);
            return default; // THIS LINE WILL NEVER RUN !!
        }

        /// <summary>
        /// Creates HttpRequestMessage object.
        /// </summary>
        /// <param name="method">Http method</param>
        /// <param name="url">request url</param>
        /// <returns>Http request message</returns>
        private HttpRequestMessage CreateRequest(HttpMethod method, string url)
        {
            string requestUrl;
            if (string.IsNullOrEmpty(url))        // caller didnt specify url, 
                requestUrl = UrlBuilder.PopUrl(); // so lets take it from UrlBuilder
            else requestUrl = url;
            return new HttpRequestMessage(method, requestUrl);
        }

        /// <summary>
        /// Sends the request to API. Request can be
        /// cancelled by cancellation token.
        /// </summary>
        /// <param name="request">Http request message</param>
        /// <param name="cToken">cancellation token</param>
        /// <returns>Http response</returns>
        private async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request, CToken cToken)
        {
            return await Client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Validates unix timestamp. If timestamp is invalid,
        /// return current unix timestamp.
        /// </summary>
        /// <param name="timestamp">unix timestamp</param>
        /// <returns>
        /// If timestamp is valid => same unix timestamp
        /// if timestamp is not valid => current datetime as unix timestamp.
        /// </returns>
        protected ulong ValidateTimestamp(long timestamp)
        {
            return timestamp > 0 ? (ulong)timestamp : GetUnixTimestampNow();
        }

        /// <summary>
        /// Returns current datetime as unix timestamp.
        /// </summary>
        /// <returns>Current datetime as unix timestamp</returns>
        protected ulong GetUnixTimestampNow()
        {
            return (ulong)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        /// <summary>
        /// Converts datetime object to unix timestamp.
        /// </summary>
        /// <param name="dateTime">datetime object</param>
        /// <returns>unix timestamp from datetime param</returns>
        protected ulong GetUnixTimestampFromDate(DateTime dateTime)
        {
            return (ulong)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }
    }
}
