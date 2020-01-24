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

namespace SteamWebRequest
{
    public partial class SteamHttpClient
    {
        public static HttpClient Client { get; } = new HttpClient();

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
            var uBuilder = new UrlBuilder(GET_PRODUCTS_URL,
                new QueryParam("key", this.DevKey),
                new QueryParam("max_results", "1"));

            HttpResponseMessage resp = Client.GetAsync(uBuilder.Url)
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
        public async Task<T> RequestAndDeserialize<T>(string url, CToken token = default)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            using (var response = await Client.SendAsync(request,
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
        public async Task<Bitmap> GetImageAsync(string url, CToken token = default)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, url))
            using (var response = await Client.SendAsync(request,
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
                    { Content = content, StatusCode = (int)response.StatusCode };
                }
            }
        }
    }
}
