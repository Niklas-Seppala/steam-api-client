using SteamApi.Models.Steam;
using SteamApi.Models.Steam.ResponseModels;
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
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>SteamServerInfo object.</returns>
        public async Task<SteamServerInfo> GetSteamServerInfoAsync(string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_WEB_API, "GetServerInfo", version);

            return await GetModelAsync<SteamServerInfo>(cToken: cToken)
                .ConfigureAwait(false);
        }

        /// <summary>
        /// Sends GET request for supported Api list. Request can
        /// be cancelled providing CancellationToken.
        /// </summary>
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>ReadOnlyCollection of ApiInterface objects</returns>
        public async Task<IReadOnlyList<ApiInterface>> GetApiListAsync(string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_WEB_API, "GetSupportedAPIList", version);

            var response = await GetModelAsync<ApiListResponse>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Apilist.Interfaces;
        }
        #endregion

        #region [Get Steam Products]

        /// <summary>
        /// Sends GET request for any kinds of products in Steam store.
        /// Request can be cancelled by providing cancellation token.
        /// </summary>
        /// <param name="products">What products to include</param>
        /// <param name="callSize">Call size</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>ReadOnlyCollection of SteamProduct objects</returns>
        public async Task<IReadOnlyList<SteamProduct>> GetSteamProductsAsync(IncludeProducts products,
            ProductCallSize callSize = ProductCallSize.Default, CToken cToken = default)
        {
            if (products.Equals(IncludeProducts.None))
                return new List<SteamProduct>();
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
                    var response = await GetModelAsync<SteamProductsContainer>(cToken: cToken)
                        .ConfigureAwait(false);
                    return response.Content.ProductList;
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
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellation token</param>
        /// <param name="steamId64s">Array of 64-bit steam-ids</param>
        /// <returns>Dictionary of Account bans</returns>
        public async Task<IReadOnlyDictionary<string, IReadOnlyCollection<AccountBans>>> GetSteamAccountsBansAsync(
            string version = "v1", CToken cToken = default, params string[] steamId64s)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_USER, "GetPlayerBans", version)
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
        /// <param name="id64">64-bit steam id</param>
        /// <param name="cToken">Cancellation token</param>
        /// <param name="version">API method version</param>
        /// <returns>List of SteamAccounts</returns>
        public async Task<IReadOnlyList<SteamAccount>> GetSteamAccountsAsync(string version = "v2",
            CToken cToken = default, params string[] id64)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_USER, "GetPlayerSummaries", version)
                .AddQuery("key", ApiKey)
                .AddQuery("steamids", string.Join(",", id64));

            var response = await GetModelAsync<AccountCollectionResponse>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Content.Accounts;
        }

        /// <summary>
        /// Sends GET request to http://api.steampowered.com/
        /// for steam profile information. Request can be cancelled
        /// by providing cancellation token.
        /// </summary>
        /// <param name="id64">64-bit steam-id</param>
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>Steam account object</returns>
        public async Task<SteamAccount> GetSteamAccountAsync(string id64, string version = "v2",
            CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_USER, "GetPlayerSummaries", version)
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
        /// <param name="id64">64-bit steam-id</param>
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>List of Friend objects</returns>
        public async Task<IReadOnlyList<Friend>> GetFriendslistAsync(long id64,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_USER, "GetFriendList", version)
                .AddQuery("key", ApiKey)
                .AddQuery("steamid", id64.ToString());

            var response = await GetModelAsync<FriendslistResponse>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Content.Friends;
        }

        /// <summary>
        /// Sends GET request for steam user's profile picture. Request can
        /// be cancelled by providing cancellation token
        /// </summary>
        /// <param name="url">Profile picture url</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>Steam profile picture as bytes</returns>
        public async Task<byte[]> GetProfilePicBytesAsync(string url, CToken cToken = default)
        {
            return await GetBytesAsync(url, cToken)
                .ConfigureAwait(false);
        }

        #endregion
    }
}
