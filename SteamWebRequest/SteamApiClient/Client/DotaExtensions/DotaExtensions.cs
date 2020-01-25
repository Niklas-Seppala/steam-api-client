using SteamApiClient.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Http;
using System.Text;
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
        private const string GET_ABILITY_DATA_URL = "http://www.dota2.com/jsfeed/abilitydata";
        private const string GET_UNIQUE_USERS_URL = "http://www.dota2.com/jsfeed/uniqueusers";
        private const string GET_ITEM_IMG_URL = "http://media.steampowered.com/apps/dota2/images/items/";
        private const string GET_HERO_IMG_URL = "http://media.steampowered.com/apps/dota2/images/heroes/";
        private const string GET_LEADERBOARD_URL = "http://www.dota2.com/webapi/ILeaderboard/GetDivisionLeaderboard/v0001/";

        #endregion

        #region [Leaderboards]

        /// <summary>
        /// Sends GET request for dota 2 leaderboards. Specify
        /// region. Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="region">leaderboard region</param>
        /// <param name="token">cancellation token</param>
        /// <returns>Leaderboard object</returns>
        /// <exception cref="APIException">Thrown when api call fails</exception>
        public static async Task<Leaderboard> GetLeaderboardAsync(this SteamHttpClient client,
            Models.Region region = default, CToken token = default)
        {
            var uBuilder = new UrlBuilder(GET_LEADERBOARD_URL);

            switch (region)
            {
                case Models.Region.Europe:
                    uBuilder.AddQuery(("division", "europe"));
                    break;
                case Models.Region.America:
                    uBuilder.AddQuery(("division", "americas"));
                    break;
                case Models.Region.SEA:
                    uBuilder.AddQuery(("division", "se_asia"));
                    break;
                case Models.Region.China:
                    uBuilder.AddQuery(("division", "china"));
                    break;
            }
            return await client.RequestAndDeserialize<Leaderboard>(
                uBuilder.Url, token).ConfigureAwait(false);
        }


        #endregion

        #region [Get Abilities]

        /// <summary>
        /// Sends GET request for dota 2 hero abilities.
        /// Request can be cancelled providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">cancellation token</param>
        /// <returns>Dictionary of abilities</returns>
        public static async Task<Dictionary<string, Ability>> GetAbilitiesDictAsync(this SteamHttpClient client,
            CToken token = default)
        {
            var abilities = await client.RequestAndDeserialize<Abilities>(GET_ABILITY_DATA_URL, token)
                .ConfigureAwait(false);
            return abilities.AbilityDict;
        }

        #endregion

        #region [Get Heroes]

        /// <summary>
        /// Sends GET request for dota heroinfo. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">cancellation token</param>
        /// <returns>Dictionary of heroinfos</returns>
        public static async Task<Dictionary<string, HeroInfo>> GetHeroInfoDictAsync(
            this SteamHttpClient client, CToken token = default)
        {
            return await client.RequestAndDeserialize<Dictionary<string, HeroInfo>>(GET_HEROINFOS_URL, token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for dota 2 hero stats. Requst
        /// can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">cancellation token</param>
        /// <returns>Dictionary of hero stats</returns>
        /// <exception cref="APIException">Thrown when API request fails</exception>
        public static async Task<Dictionary<string, HeroStats>> GetHeroStatsDictAsync(this SteamHttpClient client,
            CToken token = default)
        {
            var uBuilder = new UrlBuilder(GET_HEROPEDIA_DATA_URL, ("feeds", "herodata"));
            var stats = await client.RequestAndDeserialize<HeroStatsContainer>(uBuilder.Url, token)
                .ConfigureAwait(false);
            return stats.HeroStats;
        }

        /// <summary>
        /// Sends GET request for dota 2 heroes. Request
        /// can be cancelled by providing cancellation token.
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
        /// Sends GET request for Dota 2 iteminfo. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">cancellation token</param>
        /// <returns>Dictionary of item infos</returns>
        /// <exception cref="APIException">Thrown when api call fails.</exception>
        public static async Task<Dictionary<string, Item>> GetItemInfoDictAsync(
            this SteamHttpClient client, CToken token = default)
        {
            var items = await client.RequestAndDeserialize<ItemDictionary>(GET_ITEMINFOS_URL, token)
                .ConfigureAwait(false);
            return items.ItemDict;
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

        #region [Get Unique Users]

        /// <summary>
        /// Sends GET request for unique user count. Request
        /// can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="token">cancellation token</param>
        /// <returns>Dictionary of unique users</returns>
        public static async Task<Dictionary<string, uint>> GetUniqueUsersAsync(
            this SteamHttpClient client, CToken token = default)
        {
            return await client.RequestAndDeserialize<Dictionary<string, uint>>(
                GET_UNIQUE_USERS_URL, token).ConfigureAwait(false);
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

        #region [Get Images]

        /// <summary>
        /// Sends GET request for hero image.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="heroName">hero name</param>
        /// <param name="imgShape">shape of the image</param>
        /// <returns>hero image</returns>
        /// <exception cref="APIException">Thrown if api request fails</exception>
        public static async Task<Image> GetHeroImageAsync(this SteamHttpClient client,
            string heroName, ImageShape imgShape = ImageShape.Horizontal)
        {
            var sBuilder = new StringBuilder(GET_HERO_IMG_URL);
            sBuilder.Append(heroName);

            switch (imgShape)
            {
                case ImageShape.Vertical:
                    sBuilder.Append("_vert.jpg");
                    break;
                case ImageShape.Full:
                    sBuilder.Append("_full.png");
                    break;
                case ImageShape.Horizontal:
                    sBuilder.Append("_lg.png");
                    break;
                case ImageShape.Small:
                    sBuilder.Append("_sb.png");
                    break;
            }
            return await client.GetImageAsync(sBuilder.ToString());
        }

        /// <summary>
        /// Sends GET request for item image.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="imgName">image name</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>item image</returns>
        /// <exception cref="APIException">Thrown if api request fails</exception>
        public static async Task<Image> GetItemImageAsync(this SteamHttpClient client,
            string imgName, CToken token = default)
        {
            var sBuilder = new StringBuilder(GET_ITEM_IMG_URL);
            sBuilder.Append(imgName);

            return await client.GetImageAsync(sBuilder.ToString(), token);
        }

        #endregion
    }
}
