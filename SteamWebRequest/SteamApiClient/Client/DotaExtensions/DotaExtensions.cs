using Newtonsoft.Json;
using SteamApiClient.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CToken = System.Threading.CancellationToken;

namespace SteamApiClient.Dota
{
    /// <summary>
    /// Extension methods for SteamHttpClient.
    /// These methods target dota 2 APIs.
    /// </summary>
    public static partial class DotaExtensions
    {
        #region [Base URLs]
        private const string GET_MATCH_HISTORY_URL = "https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/v1/";
        private const string GET_MATCH_DETAILS_URL = "https://api.steampowered.com/IDOTA2Match_570/GetMatchDetails/v1/";
        private const string GET_ITEMS_URL = "http://api.steampowered.com/IEconDOTA2_570/GetGameItems/v1/";
        private const string GET_TOP_LIVE_GAMES = "https://api.steampowered.com/IDOTA2Match_570/GetTopLiveGame/v1/";
        private const string GET_HEROES_URL = "https://api.steampowered.com/IEconDOTA2_570/GetHeroes/v0001/";
        private const string GET_HEROINFOS_URL = "http://www.dota2.com/jsfeed/heropickerdata";
        private const string GET_ITEMINFOS_URL = "http://www.dota2.com/jsfeed/itemdata";
        private const string GET_INT_PRIZEPOOL_URL = "http://www.dota2.com/jsfeed/intlprizepool";
        private const string GET_HEROPEDIA_DATA_URL = "http://www.dota2.com/jsfeed/heropediadata/";

        #endregion


        #region [Get Heroes]

        /// <summary>
        /// Sends GET request for dota heroinfo.
        /// </summary>
        /// <param name="client"></param>
        /// <returns>List of heroinfos</returns>
        public static async Task<List<HeroInfo>> GetHeroInfoAsync(this SteamHttpClient client)
        {
            string response = await SteamHttpClient.Client.GetStringAsync(GET_HEROINFOS_URL).ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<HeroInfo>>(ProcessHeroInfoResponse(response));
        }

        /// <summary>
        /// Sends GET request for dota hero stats.
        /// </summary>
        /// <param name="client"></param>
        /// <returns>List of HeroStats</returns>
        public static async Task<List<HeroStats>> GetHeroStatsAsync(this SteamHttpClient client)
        {
            var uBuilder = new UrlBuilder(GET_HEROPEDIA_DATA_URL, ("feeds", "herodata"));
            string raw = await SteamHttpClient.Client.GetStringAsync(uBuilder.Url);
            return JsonConvert.DeserializeObject<List<HeroStats>>(ProcessHeroStatsResponse(raw));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<Heroes> GetHeroesAsync(this SteamHttpClient client,
            CToken token = default)
        {
            var uBuilder = new UrlBuilder(GET_HEROES_URL, ("key", client.DevKey));
            return await client.RequestAndDeserialize<Heroes>(uBuilder.Url, token);
        }

        #endregion


        #region [Get Items]

        /// <summary>
        /// Sends GET request for Dota 2 iteminfo.
        /// </summary>
        /// <param name="client"></param>
        /// <returns>List of iteminfos</returns>
        public static async Task<List<Item>> GetItemInfoAsync(this SteamHttpClient client)
        {
            string response = await SteamHttpClient.Client
                .GetStringAsync(GET_ITEMINFOS_URL)
                .ConfigureAwait(false);
            return JsonConvert.DeserializeObject<List<Item>>(ProcessItemInfoResponse(response));
        }

        /// <summary>
        /// Get Dota 2 game items. Can be cancelled
        /// using cancellation token.
        /// </summary>
        /// <param name="token">cancellation token.</param>
        /// <returns>dota 2 game items</returns>
        [Obsolete("Use GetItemInfoAsync()")]
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


        #region [Get Live Games]

        /// <summary>
        /// Sends GET request for current top live games.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">cancellation token</param>
        /// <param name="partner">partner id</param>
        /// <returns>Top live games</returns>
        public static async Task<TopLiveGames> GetTopLiveGamesAsync(this SteamHttpClient client,
            CToken token = default, int partner = 1)
        {
            var uBuilder = new UrlBuilder(GET_TOP_LIVE_GAMES,
                ("key", client.DevKey),
                ("partner", partner.ToString()));

            return await client.RequestAndDeserialize<TopLiveGames>(uBuilder.Url, token);
        }

        #endregion


        #region [Get Tournament Data]

        /// <summary>
        /// Sends GET request for Internation pricepool.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">cancellation token</param>
        /// <returns>Dictionary of currency and money sum</returns>
        public static async Task<Dictionary<string, uint>> GetIntPrizePoolAsync(this SteamHttpClient client,
            CToken token = default)
        {
            return await client.
                RequestAndDeserialize<Dictionary<string, uint>>(GET_INT_PRIZEPOOL_URL, token);
        }
        #endregion

    }
}
