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
using System.Net.Http;
using System.Net;

namespace SteamWebRequest
{
    public static partial class SteamHttpClient
    {
        private static string _devKey;
        private static HttpClient _client;

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
                if (_client == null) _client = new HttpClient();

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
    }
}
