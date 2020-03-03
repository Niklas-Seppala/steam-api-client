using SteamApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CToken = System.Threading.CancellationToken;

namespace SteamApi
{
    /// <summary>
    /// Special HttpClient for making requests for Valve's Steam
    /// APIs. Derived from ApiClient class.
    /// </summary>
    public class SteamApiClient : ApiClient
    {
        /// <summary>
        /// SteamApiClient's URL to send test request.
        /// </summary>
        /// <see cref="ApiClient.TestUrl"/>
        protected override string TestUrl => $"https://api.steampowered.com/IStoreService/GetAppList/v1/?key={ApiKey}";

        // Constant strings for building API URL methods.
        #region URL constants
        private const string HOST = "api.steampowered.com";
        private const string ISTEAM_USER = "ISteamUser";
        private const string ISTORE_SERVICE = "IStoreService";
        private const string ISTEAM_WEB_API = "ISteamWebAPIUtil";
        private const string ISTEAM_NEWS = "ISteamNews";
        #endregion

        /// <summary>
        /// Instantiates SteamApiClient object.
        /// </summary>
        /// <param name="testConnection">default is false</param>
        /// <param name="schema">default is "https"</param>
        /// <see cref="ApiClient(bool, string)"/>
        public SteamApiClient(bool testConnection = false, string schema = "https") : base(testConnection, schema) 
        {
            // Initialization happens in parent class's constructor.
        }

        #region [Get Api Info]
        /// <summary>
        /// Sends GET request for Steam serverinfo. Request can be
        /// cancelled providing CancellationToken.
        /// </summary>
        /// <returns>SteamServerInfo object.</returns>
        public async Task<SteamServerInfo> GetSteamServerInfoAsync(CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_WEB_API, "GetServerInfo", "v1");
            return await GetModelAsync<SteamServerInfo>(cToken: cToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for supported Api list. Request can
        /// be cancelled providing CancellationToken.
        /// </summary>
        /// <param name="token">cancellation token</param>
        /// <returns>ReadOnlyCollection of ApiInterface objects</returns>
        public async Task<IReadOnlyList<ApiInterface>> GetApiListAsync(CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_WEB_API, "GetSupportedAPIList", "v1");
            return (await GetModelAsync<ApiListResponse>(cToken: cToken).ConfigureAwait(false)).
                Apilist.Interfaces;
        }
        #endregion

        #region [Get Steam Products]
        /// <summary>
        /// Sends GET request for any kinds of products in Steam store.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <returns>ReadOnlyCollection of SteamProduct objects</returns>
        public async Task<IReadOnlyList<SteamProduct>> GetSteamProductsAsync(IncludeProducts products,
            ProductCallSize callSize = ProductCallSize.Default, CToken cToken = default)
        {
            if (products.Equals(IncludeProducts.None))
            {
                return new List<SteamProduct>();
            }
            else
            {
                bool callAll = CallSizeToString(callSize, out string size);
                CreateProductQueryString(products, size);
                if (callAll)
                {
                    SteamProductsContainer chunk;
                    SteamProductsContainer allProducts = await GetModelAsync<SteamProductsContainer>(cToken: cToken)
                        .ConfigureAwait(false);

                    while (allProducts.Content.MoreResults)
                    {
                        UrlBuilder.AddQuery("last_appid", allProducts.Content.LastId.ToString());
                        chunk = await GetModelAsync<SteamProductsContainer>(cToken: cToken).ConfigureAwait(false);
                        allProducts.Content.LastId = chunk.Content.LastId;
                        allProducts.Content.ProductList.AddRange(chunk.Content.ProductList);
                        allProducts.Content.MoreResults = chunk.Content.MoreResults;
                    }
                    return allProducts.Content.ProductList;
                }
                else
                {
                    return (await GetModelAsync<SteamProductsContainer>(cToken: cToken).ConfigureAwait(false))
                        .Content.ProductList;
                }
            }
        }

        /// <summary>
        /// Creates URL for GetSteamProducts-method.
        /// </summary>
        /// <returns>UrlBuilder object</returns>
        private void CreateProductQueryString(IncludeProducts products, string count)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTORE_SERVICE, "GetAppList", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("max_results", count)
                .AddQuery("include_games", products.HasFlag(IncludeProducts.Games) ? "1" : "0")
                .AddQuery("include_dlc", products.HasFlag(IncludeProducts.DLC) ? "1" : "0")
                .AddQuery("include_software", products.HasFlag(IncludeProducts.Software) ? "1" : "0")
                .AddQuery("include_hardware", products.HasFlag(IncludeProducts.Harware) ? "1" : "0")
                .AddQuery("include_videos", products.HasFlag(IncludeProducts.Videos) ? "1" : "0");
        }

        /// <summary>
        /// Converts ProductCallSize enum integer value
        /// to string.
        /// </summary>
        /// <returns>True of operation was success</returns>
        private bool CallSizeToString(ProductCallSize callSize, out string count)
        {
            bool callAll = callSize == ProductCallSize.All;
            count = callAll ? ((int)ProductCallSize.Max).ToString() : ((int)callSize).ToString();
            return callAll;
        }

        /// <summary>
        /// Sends http GET request to http://api.steampowered.com/
        /// for steam application news.
        /// </summary>
        /// <returns>AppNewsCollection object</returns>
        public async Task<AppNewsCollection> GetAppNewsAsync(
             uint appId, ushort count = 20, long endDateTimestamp = -1, CToken cToken = default, params string[] tags)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_NEWS, "GetNewsForApp", "v2")
                .AddQuery("appid", appId.ToString())
                .AddQuery("count", count.ToString())
                .AddQuery("enddate", ValidateTimestamp(endDateTimestamp).ToString())
                .AddQuery("tags", string.Join(",", tags));

            return (await GetModelAsync<AppNewsResponse>(cToken: cToken)
                .ConfigureAwait(false)).AppNews;
        }
        #endregion

        #region [Steam Users]
        /// <summary>
        /// Sends http GET request to http://api.steampowered.com/ for 
        /// Steam accounts Ban history.
        /// </summary>
        /// <returns>Dictionary of Account bans</returns>
        public async Task<IReadOnlyDictionary<string, IReadOnlyCollection<AccountBans>>> GetSteamAccountsBansAsync(
            CToken cToken = default, params string[] steamId64s)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_USER, "GetPlayerBans", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("steamids", string.Join(",", steamId64s));

            return await GetModelAsync<IReadOnlyDictionary<string, IReadOnlyCollection<AccountBans>>>(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request to http://api.steampowered.com/
        /// for steam accounts. Request can be cancelled by providing cancellation
        /// token.
        /// </summary>
        /// <returns>List of SteamAccounts</returns>
        public async Task<IReadOnlyList<SteamAccount>> GetSteamAccountsAsync(CToken cToken = default,
            params string[] id64)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_USER, "GetPlayerSummaries", "v2")
                .AddQuery("key", ApiKey)
                .AddQuery("steamids", string.Join(",", id64));

            return (await GetModelAsync<AccountCollectionResponse>(cToken: cToken)
                .ConfigureAwait(false)).Content.Accounts;
        }

        /// <summary>
        /// Sends GET request to http://api.steampowered.com/
        /// for steam profile information. Request can be cancelled
        /// by providing cancellation token.
        /// </summary>
        /// <returns>Steam account object</returns>
        public async Task<SteamAccount> GetSteamAccountAsync(string id64, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_USER, "GetPlayerSummaries", "v2")
                .AddQuery("key", ApiKey)
                .AddQuery("steamids", id64);

            var response = await GetModelAsync<AccountCollectionResponse>(cToken: cToken)
                .ConfigureAwait(false);

            if (response.Content.Accounts.Count > 0)
                return response.Content.Accounts.ElementAt(0);
            else
                return null;
        }

        /// <summary>
        /// Sends GET request for steam user's friendslist. Request
        /// can be cancelled providing cancellation Token.
        /// </summary>
        /// <returns>List of Friend objects</returns>
        public async Task<IReadOnlyList<Friend>> GetFriendslistAsync(string id64, CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_USER, "GetFriendList", "v1")
                .AddQuery("key", ApiKey)
                .AddQuery("steamid", id64);

            return (await GetModelAsync<FriendslistResponse>(cToken: cToken).ConfigureAwait(false)).Content.Friends;
        }

        /// <summary>
        /// Sends GET request for steam user's friendslist. Request
        /// can be cancelled providing cancellation Token.
        /// </summary>
        /// <returns>List of Friend objects</returns>
        public async Task<IReadOnlyList<Friend>> GetFriendslistAsync(long id64, CToken cToken = default)
        {
            return await GetFriendslistAsync(id64.ToString(), cToken);
        }

        /// <summary>
        /// Sends GET request for steam user's profile picture. Request can
        /// be cancelled by providing cancellation token
        /// </summary>
        /// <returns>Steam profile picture as bytes</returns>
        public async Task<byte[]> GetProfilePicBytesAsync(string url, CToken cToken = default)
        {
            return await GetBytesAsync(url, cToken).ConfigureAwait(false);
        }
        #endregion
    }
}
