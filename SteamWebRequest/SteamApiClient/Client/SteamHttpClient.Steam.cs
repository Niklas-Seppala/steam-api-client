using SteamApiClient.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using CToken = System.Threading.CancellationToken;


namespace SteamApiClient
{
    public partial class SteamHttpClient
    {
        #region [Base URLs]
        private const string GET_STEAM_ACCOUNT_URL = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/";
        private const string GET_STEAM_USER_FRIENDS_URL = "http://api.steampowered.com/ISteamUser/GetFriendList/v1";
        private const string GET_PRODUCTS_URL = "https://api.steampowered.com/IStoreService/GetAppList/v1/";
        private const string GET_SERVERINFO_URL = "https://api.steampowered.com/ISteamWebAPIUtil/GetServerInfo/v1/";
        private const string GET_API_LIST_URL = "https://api.steampowered.com/ISteamWebAPIUtil/GetSupportedAPIList/v1/";
        private const string GET_APP_NEWS_V2 = "https://api.steampowered.com/ISteamNews/GetNewsForApp/v2/";
        private const string GET_PLAYER_BANS = "https://api.steampowered.com/ISteamUser/GetPlayerBans/v1/";


        #endregion

        #region [Get Api Info]

        /// <summary>
        /// Sends GET request for Steam serverinfo. Request can be
        /// cancelled providing CancellationToken.
        /// </summary>
        /// <param name="token">Cancellation token</param>
        /// <returns>SteamServerInfo object.</returns>
        public async Task<SteamServerInfo> GetSteamServerInfoAsync(CToken token = default)
        {
            return await this.RequestAndDeserialize<SteamServerInfo>(GET_SERVERINFO_URL, token)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for supported Api list. Request can
        /// be cancelled providing CancellationToken.
        /// </summary>
        /// <param name="token">cancellation token</param>
        /// <returns>ReadOnlyCollection of ApiInterface objects</returns>
        public async Task<IReadOnlyCollection<ApiInterface>> GetApiListAsync(CToken token = default)
        {
            var response = await this.RequestAndDeserialize<ApiListResponse>(GET_API_LIST_URL, token)
                .ConfigureAwait(false);
            return response.Apilist.Interfaces;
        }

        #endregion

        #region [Get Steam Products]

        /// <summary>
        /// Sends GET request for any kinds of products in Steam store.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="products">product flags</param>
        /// <param name="callSize">call size</param>
        /// <param name="token">cancellation token.</param>
        /// <returns>ReadOnlyCollection of SteamProduct objects</returns>
        public async Task<IReadOnlyCollection<SteamProduct>> GetSteamProductsAsync(
            IncludeProducts products, ProductCallSize callSize = ProductCallSize.Default, CToken token = default)
        {
            if (products.Equals(IncludeProducts.None))
            {
                return new List<SteamProduct>();
            }
            else
            {
                bool callAll = this.CallSizeToString(callSize, out string size);
                var uBuilder = this.CreateProductQueryString(products, size);
                if (callAll)
                {
                    SteamProductsContainer chunk;
                    SteamProductsContainer allProducts = await this.RequestAndDeserialize<SteamProductsContainer>(uBuilder.Url, token)
                        .ConfigureAwait(false);

                    while (allProducts.Content.MoreResults)
                    {
                        uBuilder.AddQuery(("last_appid", allProducts.Content.LastId.ToString()));
                        chunk = await this.RequestAndDeserialize<SteamProductsContainer>(uBuilder.Url, token).ConfigureAwait(false);
                        allProducts.Content.LastId = chunk.Content.LastId;
                        allProducts.Content.ProductList.AddRange(chunk.Content.ProductList);
                        allProducts.Content.MoreResults = chunk.Content.MoreResults;
                    }
                    return allProducts.Content.ProductList;
                }
                else
                {
                    var response = await this.RequestAndDeserialize<SteamProductsContainer>(uBuilder.Url, token)
                        .ConfigureAwait(false);
                    return response.Content.ProductList;
                }
            }
        }

        /// <summary>
        /// Creates URL for GetSteamProducts-method.
        /// </summary>
        /// <param name="products">Product flags</param>
        /// <param name="size">call size</param>
        /// <returns>UrlBuilder object</returns>
        private UrlBuilder CreateProductQueryString(IncludeProducts products, string size)
        {
            return new UrlBuilder(GET_PRODUCTS_URL, ("max_results", size), ("key", this.DevKey),
               ("include_games", products.HasFlag(IncludeProducts.Games) ? "1" : "0"),
               ("include_dlc", products.HasFlag(IncludeProducts.DLC) ? "1" : "0"),
               ("include_software", products.HasFlag(IncludeProducts.Software) ? "1" : "0"),
               ("include_hardware", products.HasFlag(IncludeProducts.Harware) ? "1" : "0"),
               ("include_videos", products.HasFlag(IncludeProducts.Videos) ? "1" : "0"));
        }

        /// <summary>
        /// Converts ProductCallSize enum integer value
        /// to string.
        /// </summary>
        /// <param name="callSize">Callsize</param>
        /// <param name="size">result</param>
        /// <returns>True of operation was success</returns>
        private bool CallSizeToString(ProductCallSize callSize, out string size)
        {
            bool callAll = callSize == ProductCallSize.All;
            size = callAll ? ((int)ProductCallSize.Max).ToString() : ((int)callSize).ToString();
            return callAll;
        }

        #endregion

        #region [Get Steam-user friendslist]

        /// <summary>
        /// Sends GET request for steam user's friendslist. Request
        /// can be cancelled providing CancellationToken.
        /// </summary>
        /// <param name="id64">64-bit steamid</param>
        /// <param name="token">cancellation token</param>
        /// <returns>Friendslist object.</returns>
        public async Task<IReadOnlyCollection<Friend>> GetFriendslistAsync(string id64, CToken token = default)
        {
            var url = new UrlBuilder(GET_STEAM_USER_FRIENDS_URL,
                ("key", this.DevKey), ("steamid", id64));
            var response = await this.RequestAndDeserialize<FriendslistResponse>(url.Url, token)
                .ConfigureAwait(false);
            return response.Content.Friends;
        }

        /// <summary>
        /// Sends GET request for steam user's friendslist. Request
        /// can be cancelled providing CancellationToken.
        /// </summary>
        /// <param name="id64">64-bit steamid</param>
        /// <param name="token">cancellation token</param>
        /// <returns>Friendslist object.</returns>
        public async Task<IReadOnlyCollection<Friend>> GetFriendslistAsync(long id64, CToken token = default)
        {
            return await this.GetFriendslistAsync(id64.ToString(), token);
        }

        #endregion

        #region [Get SteamAccounts]

        public async Task<IReadOnlyDictionary<string, IReadOnlyCollection<AccountBans>>> GetSteamAccountBansAsync(
            CToken token = default, params string[] steamId64s)
        {
            UrlBuilder uBuilder = new UrlBuilder(GET_PLAYER_BANS,
                ("key", this.DevKey),
                ("steamids", string.Join(",", steamId64s)));

            return await this.RequestAndDeserialize<IReadOnlyDictionary<string, IReadOnlyCollection<AccountBans>>>(uBuilder.Url, token)
                .ConfigureAwait(false);
        }


        /// <summary>
        /// Sends GET request to http://api.steampowered.com/
        /// for steam accounts.
        /// </summary>
        /// <param name="id64">64-bit ids of accounts you want.</param>
        /// <returns>Collection of steam accounts</returns>
        public async Task<IReadOnlyCollection<SteamAccount>> GetSteamAccountsAsync(
            CToken token = default, params string[] id64)
        {
            var uBuilder = new UrlBuilder(GET_STEAM_ACCOUNT_URL,
                ("key", this.DevKey), ("steamids", string.Join(",", id64)));
            var response = await this.RequestAndDeserialize<AccountCollectionResponse>(uBuilder.Url, CToken.None)
                .ConfigureAwait(false);
            return response.Content.Accounts;
        }

        #endregion

        #region [Get App news]

        public async Task<AppNewsCollection> GetAppNewsAsync(
             uint appId, ushort count = 20, long endDateTimestamp = -1, CToken token = default, params string[] tags)
        {
            UrlBuilder uBuilder = new UrlBuilder(GET_APP_NEWS_V2,
                ("appid", appId.ToString()),
                ("count", count.ToString()),
                ("enddate", this.GetTimestamp(endDateTimestamp).ToString()),
                ("tags", string.Join(",", tags)));

            var response = await this.RequestAndDeserialize<AppNewsResponse>(uBuilder.Url, token)
                .ConfigureAwait(false);

            return response.AppNews;
        }

        #endregion

    }
}
