using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using SteamWebRequest.Models;

namespace SteamWebRequest
{
    public static partial class SteamHttpClient
    {
        
        public static async Task<MatchHistory> GetMatchHistoryAsync(int callSize,
            CancellationToken cancellationToken = default)
        {
            UrlBuilder urlBuilder = new UrlBuilder(
                "https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/",
                new QueryParam("key", _devKey),
                new QueryParam("matches_requested", callSize.ToString()));

            using (var request = new HttpRequestMessage(HttpMethod.Get, urlBuilder.Url))
            using (var response = await _client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false))
            using (Stream stream  = await response.Content.ReadAsStreamAsync())
            {
                if (response.IsSuccessStatusCode)
                    return DeserializeJsonStream<MatchHistory>(stream);
                else
                {
                    string content = await ReadStreamAsync(stream);
                    throw new APIException() { Content = content, StatusCode = (int)response.StatusCode };
                }
            }
        }

        /// <summary>
        /// Sends a GET request to https://api.steampowered.com/ for
        /// Dota 2 palyer's match history. Request can be cancelled by providing
        /// CancellationToken.
        /// </summary>
        /// <param name="id32">Player's steam id in 32-bit</param>
        /// <param name="callSize">Match quantity (1-100)</param>
        /// <param name="cancelToken">Cancellation token for API request</param>
        /// <returns>MatchHistory object</returns>
        /// <exception cref="HttpRequestException">Thrown when HttpRequest fails</exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="APIException">Thrown if user has private game records.</exception>
        public static async Task<MatchHistory> GetMatchHistoryAsync(string id32,
            int callSize, CancellationToken cancelToken = default)
        {
            UrlBuilder urlBuilder = new UrlBuilder(
                "https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/",
                new QueryParam("key", _devKey),
                new QueryParam("account_id", id32),
                new QueryParam("matches_requested", callSize.ToString()));

            using (var request = new HttpRequestMessage(HttpMethod.Get, urlBuilder.Url))
            using (var response = await _client.SendAsync(request,
                HttpCompletionOption.ResponseHeadersRead, cancelToken).ConfigureAwait(false))
            using (Stream stream = await response.Content.ReadAsStreamAsync())
            {
                if (response.IsSuccessStatusCode)
                    return DeserializeJsonStream<MatchHistory>(stream);
                else
                {
                    string content = await ReadStreamAsync(stream);
                    throw new APIException() { Content = content, StatusCode = (int)response.StatusCode };
                }
            }
        }
    }
}
