using SteamApi.Models.Steam;
using SteamApi.Models.Steam.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CToken = System.Threading.CancellationToken;

namespace SteamApi
{
    /// <summary>
    /// Specialized HttpClient for making requests for Valve's Steam
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
        /// Sends http GET request to api.steampowered.com
        /// for steam application news.
        /// </summary>
        /// <returns>AppNewsCollection object</returns>
        /// <exception cref="EmptyApiResultException{AppNewsCollection}"></exception>
        /// <exception cref="ApiException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<AppNewsCollection> GetAppNewsAsync(
             uint appId, uint count = 20, long endDateTimestamp = -1, CToken cToken = default, params string[] tags)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_NEWS, "GetNewsForApp", "v2")
                .AddQuery("appid", appId.ToString())
                .AddQuery("count", count.ToString())
                .AddQuery("enddate", ValidateTimestamp(endDateTimestamp).ToString())
                .AddQuery("tags", string.Join(",", tags));

            try
            {
                var response = await GetModelAsync<AppNewsResponse>(cToken: cToken)
                    .ConfigureAwait(false);

                return response.AppNews;
            }
            catch (ApiException apiEx)
            {
                // API creators did really excellet job, invalid id results to Forbidden HTTP status code :)
                if (apiEx.StatusCode == 403)
                    throw new EmptyApiResultException<AppNewsCollection>($"App id is propably invalid: {appId}", apiEx);
                else // rethrow
                    throw;
            }
        }

        #endregion

        #region [Steam Users]

        /// <summary>
        /// Sends http GET request to api.steampowered.com for 
        /// Steam accounts Ban history.
        /// </summary>
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellation token</param>
        /// <param name="steamId64s">Array of 64-bit steam-ids</param>
        /// <returns>List of account bans</returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<IReadOnlyCollection<AccountBans>> GetSteamAccountsBansAsync(
            IEnumerable<ulong> id64s, string version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_USER, "GetPlayerBans", version)
                .AddQuery("key", ApiKey)
                .AddQuery("steamids", string.Join(",", id64s));

            var response = await GetModelAsync<IReadOnlyDictionary<string, IReadOnlyCollection<AccountBans>>>(cToken: cToken)
                .ConfigureAwait(false);

            return response["players"];
        }


        /// <summary>
        /// Sends http GET request to api.steampowered.com for 
        /// a single Steam accounts ban history.
        /// </summary>
        /// <param name="id64">64-bit steam id</param>
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>Account ban model</returns>
        /// <exception cref="EmptyApiResultException{AccountBans}"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        public async Task<AccountBans> GetSteamAccountBansAsync(ulong id64, string
            version = "v1", CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_USER, "GetPlayerBans", version)
                .AddQuery("key", ApiKey)
                .AddQuery("steamids", id64.ToString());

            var response = await GetModelAsync<IReadOnlyDictionary<string, IReadOnlyList<AccountBans>>>(cToken: cToken)
                .ConfigureAwait(false);

            if (response["players"].Count == 0)
                throw new EmptyApiResultException<AccountBans>($"64-bit steam id: {id64}");
            else
                return response["players"][0];
        }


        /// <summary>
        /// Sends GET request to api.steampowered.com
        /// for steam accounts. Request can be cancelled by providing cancellation
        /// token.
        /// </summary>
        /// <param name="id64">64-bit steam id</param>
        /// <param name="cToken">Cancellation token</param>
        /// <param name="version">API method version</param>
        /// <returns>List of SteamAccounts</returns>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <example>
        /// <code>
        ///     ApiClient.SetApiKey("api key here");
        ///     var client = new SteamApiClient();
        ///     var accounts = await client.GetSteamAccountsAsync(76561198096280303, 76561198006693873,
        ///         version: "v2", cToken: CancellationToken.None);
        /// </code>
        /// </example>
        public async Task<IReadOnlyList<SteamAccount>> GetSteamAccountsAsync(IEnumerable<ulong> id64s, string version = "v2",
            CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_USER, "GetPlayerSummaries", version)
                .AddQuery("key", ApiKey)
                .AddQuery("steamids", string.Join(",", id64s));

            var response = await GetModelAsync<AccountCollectionResponse>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Content.Accounts;
        }


        /// <summary>
        /// Sends GET request to api.steampowered.com
        /// for steam profile information. Request can be cancelled
        /// by providing cancellation token.
        /// </summary>
        /// <param name="id64">64-bit steam-id</param>
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>Steam account object</returns>
        /// <exception cref="EmptyApiResultException{SteamAccount}"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <example>
        /// <code>
        ///     ApiClient.SetApiKey("api key here");
        ///     var client = new SteamApiClient();
        ///     var accounts = await client.GetSteamAccountAsync(76561198006693873,
        ///         version: "v2", cToken: CancellationToken.None);
        /// </code>
        /// </example>
        public async Task<SteamAccount> GetSteamAccountAsync(ulong id64, string version = "v2",
            CToken cToken = default)
        {
            UrlBuilder.SetHost(HOST).SetPath(ISTEAM_USER, "GetPlayerSummaries", version)
                .AddQuery("key", ApiKey)
                .AddQuery("steamids", id64.ToString());

            var response = await GetModelAsync<AccountCollectionResponse>(cToken: cToken)
                .ConfigureAwait(false);

            if (response.Content.Accounts.Count == 0)
                throw new EmptyApiResultException<SteamAccount>($"64-bit steam id: {id64}");                
            else
                return response.Content.Accounts.ElementAt(0);
        }


        /// <summary>
        /// Sends GET request for steam user's friendslist. Request
        /// can be cancelled providing cancellation Token.
        /// </summary>
        /// <param name="id64">64-bit steam-id</param>
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellation token</param>
        /// <exception cref="PrivateApiContentException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns>List of Friend objects</returns>
        /// <example>
        /// <code>
        ///     ApiClient.SetApiKey("api key here");
        ///     var client = new SteamApiClient();
        ///     var response = await client.GetFriendslistAsync(76561198049624886,
        ///         version: "v1", cToken: CancellationToken.None);
        /// </code>
        /// </example>
        public async Task<IReadOnlyList<Friend>> GetFriendslistAsync(ulong id64,
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
        /// <exception cref="ApiResourceNotFoundException"></exception>
        /// <exception cref="HttpRequestException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <example>
        /// <code>
        ///     var client = new SteamApiClient();
        ///     byte[] picBytes = await client.GetProfilePicBytesAsync("https://url.com/pic.png",
        ///         cToken: CancellationToken.None);
        /// </code>
        /// </example>
        public async Task<byte[]> GetProfilePicBytesAsync(string url, CToken cToken = default)
        {
            return await GetBytesAsync(url, cToken)
                .ConfigureAwait(false);
        }

        #endregion
    }
}
