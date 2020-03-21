using SteamApi.Responses.Steam.ParentResponses;
using SteamApi.Responses.Steam;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CToken = System.Threading.CancellationToken;
using SteamApi.Responses;

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
        public async Task<SteamServerInfoResponse> GetSteamServerInfoAsync(string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = HOST;
            UrlBuilder.AppendPath(ISTEAM_WEB_API, "GetServerInfo", version);

            string url = UrlBuilder.PopEncodedUrl(false);
            SteamServerInfoResponse result = null;
            Exception exception = null;
            try
            {
                var response = await GetModelAsync<SteamServerInfo>(url, cToken)
                    .ConfigureAwait(false);
                result = new SteamServerInfoResponse()
                {
                    Contents = response
                };
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return WrapResponse(result, url, exception);
        }


        /// <summary>
        /// Sends GET request for supported Api list. Request can
        /// be cancelled providing CancellationToken.
        /// </summary>
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>ApiListResponse object</returns>
        public async Task<ApiListResponse> GetApiListAsync(string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = HOST;
            UrlBuilder.AppendPath(ISTEAM_WEB_API, "GetSupportedAPIList", version);

            string url = UrlBuilder.PopEncodedUrl(false);
            ApiListResponse result = null;
            Exception exception = null;
            try
            {
                var response = await GetModelAsync<ApiListResponseParent>(url, cToken)
                    .ConfigureAwait(false);
                result = new ApiListResponse()
                {
                    Contents = response.Apilist.Interfaces
                };
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return WrapResponse(result, url, exception);
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
        /// <returns>SteamProductResponse object</returns>
        public async Task<SteamProductsResponse> GetSteamProductsAsync(IncludeProducts products,
            ProductCallSize callSize = ProductCallSize.Default, CToken cToken = default)
        {
            if (products.Equals(IncludeProducts.None))
            {
                return new SteamProductsResponse() { Successful = false };
            }
            else
            {
                bool callAll = CallSizeToString(callSize, out string size);
                CreateProductUrl(products, size);

                string originalUrl = UrlBuilder.GetEncodedUrl();
                SteamProductsResponse result = null;
                Exception exception = null;

                if (callAll)
                {
                    (result, exception) = await GetAllProducts(result, exception, cToken);
                }
                else
                {
                    (result, exception) = await GetProducts(result, exception, cToken);
                }
                return WrapResponse(result, originalUrl, exception);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="thrown"></param>
        /// <param name="cToken"></param>
        /// <returns></returns>
        private async Task<(SteamProductsResponse, Exception)> GetProducts(
            SteamProductsResponse result, Exception thrown, CToken cToken)
        {
            try
            {
                var response = await GetModelAsync<SteamProductsResponseParent>(UrlBuilder.PopEncodedUrl(false), cToken)
                    .ConfigureAwait(false);
                result = new SteamProductsResponse()
                {
                    Contents = response.Content.ProductList,
                    LastAppId = response.Content.LastId,
                    MoreResults = response.Content.MoreResults
                };
            }
            catch (Exception ex)
            {
                thrown = ex;
            }
            return (result, thrown);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <param name="thrown"></param>
        /// <param name="cToken"></param>
        /// <returns></returns>
        private async Task<(SteamProductsResponse, Exception)> GetAllProducts(
            SteamProductsResponse result, Exception thrown,  CToken cToken)
        {
            try
            {
                SteamProductsResponseParent chunk = null;
                SteamProductsResponseParent allProducts =
                    await GetModelAsync<SteamProductsResponseParent>(UrlBuilder.GetEncodedUrl(), cToken: cToken)
                        .ConfigureAwait(false);

                while (allProducts.Content.MoreResults)
                {
                    UrlBuilder.AppendQuery("last_appid", allProducts.Content.LastId.ToString());
                    chunk = await GetModelAsync<SteamProductsResponseParent>(UrlBuilder.GetEncodedUrl(), cToken: cToken)
                        .ConfigureAwait(false);
                    allProducts.Content.LastId = chunk.Content.LastId;
                    allProducts.Content.ProductList.AddRange(chunk.Content.ProductList);
                    allProducts.Content.MoreResults = chunk.Content.MoreResults;
                }
                result = new SteamProductsResponse()
                {
                    Contents = allProducts.Content.ProductList,
                    LastAppId = chunk.Content.LastId,
                    MoreResults = chunk.Content.MoreResults
                };
            }
            catch (Exception ex)
            {
                thrown = ex;
            }
            finally
            {
                UrlBuilder.PopEncodedUrl(false);
            }
            return (result, thrown);
        }


        /// <summary>
        /// Creates URL for GetSteamProducts-method.
        /// </summary>
        /// <returns>UrlBuilder object</returns>
        private void CreateProductUrl(IncludeProducts products, string count)
        {
            UrlBuilder.Host = HOST;
            UrlBuilder.AppendPath(ISTORE_SERVICE, "GetAppList", "v1");
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("max_results", count)
                .AppendQuery("include_games", products.HasFlag(IncludeProducts.Games) ? "1" : "0")
                .AppendQuery("include_dlc", products.HasFlag(IncludeProducts.DLC) ? "1" : "0")
                .AppendQuery("include_software", products.HasFlag(IncludeProducts.Software) ? "1" : "0")
                .AppendQuery("include_hardware", products.HasFlag(IncludeProducts.Harware) ? "1" : "0")
                .AppendQuery("include_videos", products.HasFlag(IncludeProducts.Videos) ? "1" : "0");
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
        public async Task<AppNewsResponse> GetAppNewsAsync(uint appId, uint count = 20,
            long endDateTimestamp = -1, CToken cToken = default, params string[] tags)
        {
            UrlBuilder.Host = HOST;
            UrlBuilder.AppendPath(ISTEAM_NEWS, "GetNewsForApp", "v2");
            UrlBuilder.AppendQuery("appid", appId.ToString())
                .AppendQuery("count", count.ToString())
                .AppendQuery("enddate", ValidateTimestamp(endDateTimestamp).ToString())
                .AppendQuery("tags", string.Join(",", tags));

            string url = UrlBuilder.PopEncodedUrl(false);
            AppNewsResponse result = null;
            Exception exception = null;
            try
            {
                var response = await GetModelAsync<AppNewsResponseParent>(url, cToken)
                    .ConfigureAwait(false);
                result = new AppNewsResponse()
                {
                    Contents = response.AppNews
                };
            }
            catch (Exception ex)
            {
                // API creators did really excellet job, invalid id results to Forbidden HTTP status code
                if (ex is ApiException apiEx && apiEx.HttpStatusCode == 403)
                {
                    exception = new ApiEmptyResultException($"App id is propably invalid: {appId}", apiEx);
                }
                else
                {
                    exception = ex;
                }
            }
            return WrapResponse(result, url, exception);
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
        public async Task<MultipleAccountBansResponse> GetSteamAccountsBansAsync(
            IEnumerable<ulong> id64s, string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = HOST;
            UrlBuilder.AppendPath(ISTEAM_USER, "GetPlayerBans", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("steamids", string.Join(",", id64s));

            string url = UrlBuilder.PopEncodedUrl(false);
            MultipleAccountBansResponse result = null;
            Exception exception = null;
            try
            {
                var response = await GetModelAsync<IReadOnlyDictionary<string, IReadOnlyList<AccountBans>>>(url, cToken)
                    .ConfigureAwait(false);

                if (response["players"].Count == 0)
                {
                    throw new ApiEmptyResultException("Players not found with provided ids");
                }
                else
                {
                    result = new MultipleAccountBansResponse()
                    {
                        Contents = response["players"]
                    };
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return WrapResponse(result, url, exception);
        }


        /// <summary>
        /// Sends http GET request to api.steampowered.com for 
        /// a single Steam accounts ban history.
        /// </summary>
        /// <param name="id64">64-bit steam id</param>
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>Account ban model</returns>
        public async Task<SteamAccountBansResponse> GetSteamAccountBansAsync(ulong id64, string
            version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = HOST;
            UrlBuilder.AppendPath(ISTEAM_USER, "GetPlayerBans", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("steamids", id64.ToString());

            string url = UrlBuilder.PopEncodedUrl(false);
            SteamAccountBansResponse result = null;
            Exception exception = null;
            try
            {
                var response = await GetModelAsync<IReadOnlyDictionary<string, IReadOnlyList<AccountBans>>>(url, cToken)
                    .ConfigureAwait(false);

                if (response["players"].Count == 0)
                {
                    throw new ApiEmptyResultException($"Player not found with provided id");
                }
                else
                {
                    result = new SteamAccountBansResponse()
                    {
                        Contents = response["players"][0]
                    };
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return WrapResponse(result, url, exception);
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
        public async Task<MultipleSteamAccountsResponse> GetSteamAccountsAsync(IEnumerable<ulong> id64s, string version = "v2",
            CToken cToken = default)
        {
            UrlBuilder.Host = HOST;
            UrlBuilder.AppendPath(ISTEAM_USER, "GetPlayerSummaries", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("steamids", string.Join(",", id64s));

            string url = UrlBuilder.PopEncodedUrl(false);
            MultipleSteamAccountsResponse result = null;
            Exception exception = null;
            try
            {
                var response = await GetModelAsync<AccountCollectionResponseParent>(url, cToken)
                    .ConfigureAwait(false);
                result = new MultipleSteamAccountsResponse()
                {
                    Contents = response.Content.Accounts
                };
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return WrapResponse(result, url, exception);
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
        public async Task<SteamAccountResponse> GetSteamAccountAsync(ulong id64, string version = "v2",
            CToken cToken = default)
        {
            UrlBuilder.Host = HOST;
            UrlBuilder.AppendPath(ISTEAM_USER, "GetPlayerSummaries", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("steamids", id64.ToString());

            string url = UrlBuilder.PopEncodedUrl(false);
            SteamAccountResponse result = null;
            Exception exception = null;
            try
            {
                var response = await GetModelAsync<AccountCollectionResponseParent>(url, cToken)
                    .ConfigureAwait(false);

                if (response.Content.Accounts.Count == 0)
                {
                    throw new ApiEmptyResultException("Account not found");
                }
                else
                {
                    result = new SteamAccountResponse()
                    {
                        Contents = response.Content.Accounts.ElementAt(0)
                    };
                }
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return WrapResponse(result, url, exception);
        }


        /// <summary>
        /// Sends GET request for steam user's friendslist. Request
        /// can be cancelled providing cancellation Token.
        /// </summary>
        /// <param name="id64">64-bit steam-id</param>
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>List of Friend objects</returns>
        public async Task<FirendsListResponse> GetFriendslistAsync(ulong id64,
            string version = "v1", CToken cToken = default)
        {
            UrlBuilder.Host = HOST;
            UrlBuilder.AppendPath(ISTEAM_USER, "GetFriendList", version);
            UrlBuilder.AppendQuery("key", ApiKey)
                .AppendQuery("steamid", id64.ToString());

            string url = UrlBuilder.PopEncodedUrl(false);
            FirendsListResponse result = null;
            Exception exception = null;
            try
            {
                var response = await GetModelAsync<FriendslistResponseParent>(url, cToken: cToken)
                    .ConfigureAwait(false);

                result = new FirendsListResponse() 
                {
                    Contents = response.Content.Friends
                };
            }
            catch (Exception ex)
            {
                exception = ex;
            }
            return WrapResponse(result, url, exception);
        }


        /// <summary>
        /// Sends GET request for steam user's profile picture. Request can
        /// be cancelled by providing cancellation token
        /// </summary>
        /// <param name="url">Profile picture url</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>Steam profile picture as bytes wrapped to ApiResponse</returns>
        public async Task<ProfileAvatarResponse> GetProfilePicBytesAsync(string url, CToken cToken = default)
        {
            ProfileAvatarResponse result = null;
            Exception exeption = null;
            try
            {
                byte[] bytes = await GetBytesAsync(url, cToken)
                    .ConfigureAwait(false);
                result = new ProfileAvatarResponse { Contents = bytes };
            }
            catch (Exception ex)
            {
                exeption = ex;
            }
            return WrapResponse(result, url, exeption);
        }

        #endregion
    }
}
