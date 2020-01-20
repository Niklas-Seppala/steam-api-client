/**
* This class is spread over three files:
* SteamHttpClient.cs (this file)
*     - Configuration and utility methods
* SteamHttpCLient.Dota2.cs
*     - Requests for dota2 web API
*  SteamHttpClient.Steam.cs
*     - Requests for steam web API
*/

using System;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace SteamWebRequest
{
    public static partial class SteamHttpClient
    {
        private static string _devKey;
        private static readonly HttpClient _client = new HttpClient();

        /// <summary>
        /// Initializes SteamHttpClient.
        /// Sets and validates developer key. Instantiates static HttpClient object.
        /// </summary>
        /// <param name="key">Private developer key</param>
        /// <exception cref="HttpClientConfigException">
        /// Thrown when initialization process fails.
        /// </exception>
        public static void ConfigureClient(string developerKey)
        {
            try
            {
                _devKey = developerKey;
                ValidateDevKey();
            }
            catch (Exception ex)
            {
                throw new HttpClientConfigException(inner: ex);
            }
        }

        /// <summary>
        /// Validates DotaHttpClient's developer key.
        /// </summary>
        /// <exception cref="HttpRequestException">
        /// Thrown when validation fails.
        /// </exception>
        private static void ValidateDevKey()
        {
            UrlBuilder url = new UrlBuilder("https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/",
                new QueryParam("key", _devKey),
                new QueryParam("matches_requested", "2"));

            HttpResponseMessage resp = _client.GetAsync(url.ToString()).Result;
            if (resp.IsSuccessStatusCode) return;
            else throw new HttpRequestException(resp.StatusCode == HttpStatusCode.Forbidden
                ? $"Response status code: {resp.StatusCode}. Developer key is invalid!"
                : $"Response status code: {resp.StatusCode}. Something went wrong with key validation.");
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
        private static async Task<string> ReadStreamAsync(Stream stream)
        {
            if (stream.CanRead)
            {
                using var streamReader = new StreamReader(stream);
                    return await streamReader.ReadToEndAsync().ConfigureAwait(false);
            }
            else throw stream == null
                ? new ArgumentNullException("Provided stream is null.")
                : new ArgumentException("Provided stream is write only.");
        }

        /// <summary>
        /// Deserializes json stream to defined model
        /// object.
        /// </summary>
        /// <typeparam name="T">Model type</typeparam>
        /// <param name="stream">json stream</param>
        /// <returns>Model object</returns>
        private static T DeserializeJsonStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                throw new ArgumentNullException("Provided stream is null.");
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
    }
}
