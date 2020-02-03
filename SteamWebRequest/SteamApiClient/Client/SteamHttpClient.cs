/**
* This class is spread over two files:
* SteamHttpClient.cs (this file)
*     - Configuration and utility methods
*  SteamHttpClient.Steam.cs
*     - Requests for steam web API
*/

using Newtonsoft.Json;
using System;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CToken = System.Threading.CancellationToken;

namespace SteamApiClient
{
    public partial class SteamHttpClient
    {
        private static HttpClient _client = new HttpClient();
        // TODO: create private UrlBuilder and reuse it every api call.

        public string DevKey { get; }

        /// <summary>
        /// Initializes SteamHttpClient.
        /// Sets and validates developer key. Instantiates static HttpClient object.
        /// </summary>
        /// <param name="key">Private developer key</param>
        /// <exception cref="HttpClientConfigException">
        /// Thrown when initialization process fails.
        /// </exception>
        public SteamHttpClient(string developerKey)
        {
            try
            {
                this.DevKey = developerKey;
                this.ValidateDevKey();
            }
            catch (Exception ex)
            {
                this.DevKey = string.Empty;
                throw new HttpClientConfigException(inner: ex);
            }
        }

        /// <summary>
        /// Validates DotaHttpClient's developer key.
        /// </summary>
        /// <exception cref="HttpRequestException">
        /// Thrown when validation fails.
        /// </exception>
        private void ValidateDevKey()
        {
            var uBuilder = new UrlBuilder(GET_PRODUCTS_URL, ("key", this.DevKey), ("max_results", "1"));
            HttpResponseMessage resp = _client.GetAsync(uBuilder.Url)
                .Result;

            if (resp.IsSuccessStatusCode)
            {
                return;
            }
            else
            {
                throw new HttpRequestException(resp.StatusCode == HttpStatusCode.Forbidden
                    ? $"Response status code: {resp.StatusCode}. Developer key is invalid!"
                    : $"Response status code: {resp.StatusCode}. Something went wrong with key validation.");
            }
        }

        /// <summary>
        /// Reads the provided stream.
        /// </summary>
        /// <param name="stream">stream</param>
        /// <returns>stream contents</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when stream is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when stream is write only.
        /// </exception>
        protected async Task<string> ReadStreamAsync(Stream stream)
        {
            if (stream.CanRead)
            {
                using (var streamReader = new StreamReader(stream))
                {
                    return await streamReader.ReadToEndAsync().ConfigureAwait(false);
                }
            }
            else
            {
                throw stream == null
                    ? new ArgumentNullException("Provided stream is null.")
                    : new ArgumentException("Provided stream is write only.");
            }
        }

        /// <summary>
        /// Deserializes json stream to defined model
        /// object.
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="stream">json stream</param>
        /// <returns>Model object</returns>
        protected T DeserializeJsonStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
            {
                throw stream == null
                    ? new ArgumentNullException("Provided stream is null.")
                    : new ArgumentException("Provided stream is write only.");
            }
            else
            {
                using (var streamReader = new StreamReader(stream))
                using (var JsonReader = new JsonTextReader(streamReader))
                {
                    var jsonSerializer = new JsonSerializer();
                    return jsonSerializer.Deserialize<T>(JsonReader);
                }
            }
        }

        /// <summary>
        /// Sends GET request to API and deserializes
        /// answer to defined model object.
        /// </summary>
        /// <typeparam name="T">Model</typeparam>
        /// <param name="url">Url for request</param>
        /// <param name="token">cancellation token</param>
        /// <returns>Deserialized json object</returns>
        internal async Task<T> RequestAndDeserialize<T>(string url, CToken token = default)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false))
            using (Stream stream = await response.Content.ReadAsStreamAsync())
            {
                if (response.IsSuccessStatusCode)
                {
                    return this.DeserializeJsonStream<T>(stream);
                }
                else
                {
                    string content = await this.ReadStreamAsync(stream);
                    throw new APIException() { Content = content, StatusCode = (int)response.StatusCode };
                }
            }
        }

        /// <summary>
        /// Gets image from privide url. Request can be cancelled
        /// by providing cancellation token.
        /// </summary>
        /// <param name="url">image url</param>
        /// <param name="token">cancellation token</param>
        /// <returns>Bitmap image</returns>
        public async Task<Image> GetImageAsync(string url, CToken token = default)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false))
            using (Stream stream = await response.Content.ReadAsStreamAsync())
            {
                if (response.IsSuccessStatusCode)
                {
                    return new Bitmap(stream);
                }
                else
                {
                    string content = await this.ReadStreamAsync(stream).ConfigureAwait(false);
                    throw new APIException("Request for image failed.")
                    { Content = content, StatusCode = (int)response.StatusCode, URL = url };
                }
            }
        }


        /// <summary>
        /// Sends get request to url. Returns response as stream.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="token">cancellation token</param>
        /// <returns>response as stream</returns>
        internal async Task<Stream> GetAsync(string url, CToken token = default)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStreamAsync();
                }
                else
                {
                    throw new APIException("Request failed.")
                    { StatusCode = (int)response.StatusCode, URL = url };
                }
            }
        }

        /// <summary>
        /// Sends get request to url. Returns response as a string.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="token">cancellation token</param>
        /// <returns>response as string</returns>
        internal async Task<string> GetStringAsync(string url, CToken token = default)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead, token).ConfigureAwait(false))
            {
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new APIException("Request failed.")
                    { StatusCode = (int)response.StatusCode, URL = url };
                }
            }
        }


        #region [Utility]

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        internal ulong GetTimestamp(long timestamp)
        {
            if (timestamp < 0)
            {
                return (ulong)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            }
            else
            {
                return (ulong)timestamp;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        internal ulong GetTimestamp(DateTime dateTime)
        {
            return (ulong)dateTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
        }

        #endregion
    }
}
