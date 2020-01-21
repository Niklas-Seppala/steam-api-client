using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using SteamWebRequest.Models;
//                               !!ALIAS!!
using CToken = System.Threading.CancellationToken;

namespace SteamWebRequest
{
    public static partial class SteamHttpClient
    {

        /// <summary>
        /// Sends a GET request to https://api.steampowered.com/ for
        /// universal match history. Request can be cancelled by providing
        /// CancellationToken.
        /// </summary>
        /// <param name="callSize">Match quantity (1-100)</param>
        /// <param name="token">Cancellation token for API request</param>
        /// <returns>MatchHistory object</returns>
        /// <exception cref="HttpRequestException">Thrown when HttpRequest fails</exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="APIException">Thrown if user has private game records.</exception>
        public static async Task<MatchHistory> GetMatchHistoryAsync(byte callSize, CToken token = default)
        {
            UrlBuilder urlBuilder = new UrlBuilder(
                "https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/",
                new QueryParam("key", _devKey),
                new QueryParam("matches_requested", callSize.ToString()));
            return await SendGETRequestAndDeserialize<MatchHistory>(urlBuilder.Url, token);
        }

        /// <summary>
        /// Sends a GET request to https://api.steampowered.com/ for
        /// Dota 2 player's match history. Request can be cancelled by providing
        /// CancellationToken.
        /// </summary>
        /// <param name="id32">Player's steam id in 32-bit</param>
        /// <param name="callSize">Match quantity (1-100)</param>
        /// <param name="cancelToken">Cancellation token for API request</param>
        /// <returns>MatchHistory object</returns>
        /// <exception cref="HttpRequestException">Thrown when HttpRequest fails</exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="APIException">Thrown if user has private game records.</exception>
        public static async Task<MatchHistory> GetMatchHistoryAsync(string id32, byte callSize, CToken token = default)
        {
            UrlBuilder urlBuilder = new UrlBuilder(
                "https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/",
                new QueryParam("key", _devKey),
                new QueryParam("account_id", id32),
                new QueryParam("matches_requested", callSize.ToString()));
            return await SendGETRequestAndDeserialize<MatchHistory>(urlBuilder.Url, token);
        }

        /// <summary>
        /// Sends GET request to https://api.steampowered.com/ for
        /// Dota 2 match details. Request can be cancelled by providing
        /// CancellationToken.
        /// </summary>
        /// <param name="matchId">Match id</param>
        /// <param name="token">Cancellation token for API request</param>
        /// <returns>MatchDetails object</returns>
        /// <exception cref="HttpRequestException">Thrown when HttpRequest fails</exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<MatchDetails> GetMatchDetailsAsync(string matchId, CToken token = default)
        {
            UrlBuilder urlBuilder = new UrlBuilder("" +
                "https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/V001/",
                new QueryParam("match_id", matchId),
                new QueryParam("key", _devKey));
            return await SendGETRequestAndDeserialize<MatchDetails>(urlBuilder.Url, token);
        }
    }
}
