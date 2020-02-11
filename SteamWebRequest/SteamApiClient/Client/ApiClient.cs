using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CToken = System.Threading.CancellationToken;

namespace SteamApi
{
    public abstract class ApiClient
    {
        private static readonly DateTime _unixEpochStartTime = new DateTime(1970, 1, 1);
        protected static HttpClient Client { get; } = new HttpClient();
        protected static string DevKey { get; private set; }
        protected private UrlBuilder UrlBuilder { get; }
        protected abstract string TestUrl { get; }

        public static void SetDeveloperKey(string devKey)
        {
            DevKey = devKey;
        }

        protected ApiClient(bool testConnection, string schema)
        {
            UrlBuilder = new UrlBuilder()
                .SetSchema(schema);
            if (testConnection)
                TestRequest().Wait();
        }

        protected async Task TestRequest()
        {
            using (HttpResponseMessage response = Client.GetAsync(this.TestUrl).Result)
            {
                await HandleResults<object>(response, (r) => null);
            }
        }

        private HttpRequestException AppropriateExceptions(HttpStatusCode code)
        {
            if (code == HttpStatusCode.Forbidden)
            {
                return new HttpRequestException($"Response status code: {code}." +
                    $" Developer key is invalid.");
            }
            else
            {
                return new HttpRequestException($"Response status code: {code}." +
                    $" Something went wrong with request.");
            }
        }

        private ArgumentException StreamNotReadableException(bool streamNull)
        {
            return streamNull
                ? new ArgumentNullException("Provided stream is null.")
                : new ArgumentException("Provided stream is write only.");
        }

        /// <summary>
        /// Sends get request to url. Returns response as stream.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <returns>response as stream</returns>
        protected async Task<Stream> GetAsync(string url = "", CToken token = default)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get,
                string.IsNullOrEmpty(url) ? UrlBuilder.PopUrl() : url))
            using (var response = await Client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false))
            {
                return await HandleResults(response, async (resp) =>
                    await resp.Content.ReadAsStreamAsync());
            }
        }

        /// <summary>
        /// Sends get request to url. Returns response as a string.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <returns>response as string</returns>
        protected async Task<string> GetStringAsync(string url = "", CToken token = default)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get,
                string.IsNullOrEmpty(url) ? UrlBuilder.PopUrl() : url))
            using (var response = await Client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false))
            {
                return await HandleResults(response, async (resp) =>
                    await resp.Content.ReadAsStringAsync());
            }
        }

        /// <summary>
        /// Sends GET request to API and deserializes
        /// response to model object.
        /// </summary>
        /// <typeparam name="T">response model</typeparam>
        /// <returns>Deserialized json object</returns>
        protected async Task<T> GetModelAsync<T>(string url = "", CToken token = default)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get,
                string.IsNullOrEmpty(url) ? UrlBuilder.PopUrl() : url))
            using (var response = await Client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false))
            {
                return await HandleResults(response, async (resp) => {
                    using (Stream s = await resp.Content.ReadAsStreamAsync())
                    {
                        return DeserializeJsonStream<T>(s);
                    }
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
                {
                    return new JsonSerializer().Deserialize<T>(JsonReader);
                }
            }
            else throw StreamNotReadableException(stream == null);
        }

        /// <summary>
        /// Handles api response based on HttpResponseMessage
        /// status code. Throws errors if needed. provide success function.
        /// </summary>
        /// <typeparam name="T">request result type</typeparam>
        /// <param name="response">api response</param>
        /// <param name="success">success function</param>
        private async Task<T> HandleResults<T>(HttpResponseMessage response,
            Func<HttpResponseMessage, Task<T>> success)
        {
            if (response.IsSuccessStatusCode)
                return await success.Invoke(response).ConfigureAwait(false);
            else
                throw AppropriateExceptions(response.StatusCode);
        }

        protected ulong ValidateTimestamp(long timestamp)
        {
            return timestamp > 0 ? (ulong)timestamp : GetUnixTimestampNow();
        }

        protected ulong GetUnixTimestampNow()
        {
            return (ulong)DateTime.UtcNow.Subtract(_unixEpochStartTime).TotalSeconds;
        }

        protected ulong GetUnixTimestampFromDate(DateTime dateTime)
        {
            return (ulong)dateTime.Subtract(_unixEpochStartTime).TotalSeconds;
        }
    }
}
