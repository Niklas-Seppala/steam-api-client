using System;
using System.Net.Http;
using System.Threading.Tasks;
using CToken = System.Threading.CancellationToken;
using SteamApiClient.Models;

namespace SteamApiClient.Dota
{
    /// <summary>
    /// Extension methods for SteamHttpClient.
    /// These methods target dota 2 APIs.
    /// </summary>
    public static class DotaExtensions
    {
        #region [Base URLs]
        private const string GET_MATCH_HISTORY_URL = "https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/v1/";
        private const string GET_MATCH_DETAILS_URL = "https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/v1/";
        private const string GET_ITEMS_URL = "http://api.steampowered.com/IEconDOTA2_570/GetGameItems/v1/";
        #endregion

        #region [Get Items]

        /// <summary>
        /// Get Dota 2 game items. Can be cancelled
        /// using cancellation token.
        /// </summary>
        /// <param name="token">cancellation token.</param>
        /// <returns>dota 2 game items</returns>
        public static async Task<GameItems> GetGameItemsAsync(this SteamHttpClient client,
            CToken token = default)
        {
            var url = new UrlBuilder(GET_ITEMS_URL, ("key", client.DevKey));
            return await client.RequestAndDeserialize<GameItems>(url.Url, token);
        }

        #endregion

        #region [Get Match History]

        /// <summary>
        /// Sends a GET request for universal match history.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="callSize">Match quantity (1-100)</param>
        /// <param name="token">Cancellation token for API request</param>
        /// <returns>MatchHistory object</returns>
        /// <exception cref="HttpRequestException">Thrown when HttpRequest fails</exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="APIException">Thrown if user has private game records.</exception>
        public static async Task<MatchHistory> GetLiveMatchHistoryAsync(this SteamHttpClient client,
            byte callSize, CToken token = default)
        {
            var urlBuilder = new UrlBuilder(GET_MATCH_HISTORY_URL,
                ("key", client.DevKey),
                ("matches_requested", callSize.ToString()));
            return await client.RequestAndDeserialize<MatchHistory>(urlBuilder.Url, token);
        }

        /// <summary>
        /// Sends a GET request for Dota 2 player's match history.
        /// Request can be cancelled by providing CancellationToken.
        /// </summary>
        /// <param name="id32">Player's steam id in 32-bit</param>
        /// <param name="callSize">Match quantity (1-100)</param>
        /// <param name="cancelToken">Cancellation token for API request</param>
        /// <returns>MatchHistory object</returns>
        /// <exception cref="HttpRequestException">Thrown when HttpRequest fails</exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="APIException">Thrown if user has private game records.</exception>
        public static async Task<MatchHistory> GetMatchHistoryAsync(this SteamHttpClient client,
            string id32, byte callSize, CToken token = default)
        {
            var urlBuilder = new UrlBuilder(GET_MATCH_HISTORY_URL,
                ("key", client.DevKey),
                ("account_id", id32),
                ("matches_requested", callSize.ToString()));
            return await client.RequestAndDeserialize<MatchHistory>(urlBuilder.Url, token);
        }

        #endregion

        #region [Get Match Details]

        /// <summary>
        /// Sends GET request for Dota 2 match details. Request can be
        /// cancelled by providing cancellation token.
        /// </summary>
        /// <param name="matchId">Match id</param>
        /// <param name="token">Cancellation token for API request</param>
        /// <returns>MatchDetails object</returns>
        /// <exception cref="HttpRequestException">Thrown when HttpRequest fails</exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static async Task<MatchDetails> GetMatchDetailsAsync(this SteamHttpClient client,
            string matchId, CToken token = default)
        {
            var urlBuilder = new UrlBuilder(GET_MATCH_DETAILS_URL,
                ("match_id", matchId),
                ("key", client.DevKey),
                ("include_persona_names", "1"));
            return await client.RequestAndDeserialize<MatchDetails>(urlBuilder.Url, token);
        }

        #endregion
    }
}
