using System.Threading.Tasks;
using SteamWebRequest.Models;
using CToken = System.Threading.CancellationToken;

namespace SteamWebRequest
{
    public partial class SteamHttpClient
    {
        #region [Base URLs]
        private const string GET_STEAM_ACCOUNT_URL = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/";
        private const string GET_STEAM_USER_FRIENDS_URL = "http://api.steampowered.com/ISteamUser/GetFriendList/v1";
        private const string GET_PRODUCTS_URL = "https://api.steampowered.com/IStoreService/GetAppList/v1/";
        #endregion

        #region [Get Steam Products]

        /// <summary>
        /// Get any kinds products on Steam store. Can be cancelled
        /// by providing cancellation token.
        /// </summary>
        /// <param name="products">product flags</param>
        /// <param name="callSize">call size</param>
        /// <param name="token">cancellation token.</param>
        /// <returns>SteamProduvts object.</returns>
        public async Task<SteamProducts> GetSteamProductsAsync(
            IncludeProducts products, ProductCallSize callSize = ProductCallSize.Default, CToken token = default)
        {
            if (products.Equals(IncludeProducts.None))
            {
                return new SteamProducts();
            }
            else
            {
                bool callAll = CallSizeToString(callSize, out string size);
                var uBuilder = CreateProductQueryString(products, size);
                if (callAll)
                {
                    SteamProducts chunk;
                    SteamProducts allProducts = await RequestAndDeserialize<SteamProducts>(uBuilder.Url, token)
                        .ConfigureAwait(false);

                    while (allProducts.Content.MoreResults)
                    {
                        uBuilder.AddQuery("last_appid", allProducts.Content.LastId.ToString());
                        chunk = await RequestAndDeserialize<SteamProducts>(uBuilder.Url, token).ConfigureAwait(false);
                        allProducts.Content.LastId = chunk.Content.LastId;
                        allProducts.Content.ProductList.AddRange(chunk.Content.ProductList);
                        allProducts.Content.MoreResults = chunk.Content.MoreResults;
                    }
                    return allProducts;
                }
                else
                {
                    return await RequestAndDeserialize<SteamProducts>(uBuilder.Url, token);
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
            return new UrlBuilder(GET_PRODUCTS_URL,
               new QueryParam("max_results", size),
               new QueryParam("key", DevKey),
               new QueryParam("include_games", products.HasFlag(IncludeProducts.Games) ? "1" : "0"),
               new QueryParam("include_dlc", products.HasFlag(IncludeProducts.DLC) ? "1" : "0"),
               new QueryParam("include_software", products.HasFlag(IncludeProducts.Software) ? "1" : "0"),
               new QueryParam("include_hardware", products.HasFlag(IncludeProducts.Harware) ? "1" : "0"),
               new QueryParam("include_videos", products.HasFlag(IncludeProducts.Videos) ? "1" : "0"));
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
        /// Get steam user's friendlist.
        /// </summary>
        /// <param name="id64">64-bit steamid</param>
        /// <param name="token">cancellation token</param>
        /// <returns>Friendslist object.</returns>
        public async Task<Friendslist> GetFriendslistAsync(string id64, CToken token = default)
        {
            var url = new UrlBuilder(GET_STEAM_USER_FRIENDS_URL,
                new QueryParam("key", DevKey),
                new QueryParam("steamid", id64));
            return await RequestAndDeserialize<Friendslist>(url.Url, token);
        }

        /// <summary>
        /// Get steam user's friendlist.
        /// </summary>
        /// <param name="id64">64-bit steamid</param>
        /// <param name="token">cancellation token</param>
        /// <returns>Friendslist object.</returns>
        public async Task<Friendslist> GetFriendslistAsync(long id64, CToken token = default)
        {
            return await GetFriendslistAsync(id64.ToString(), token);
        }

        #endregion

        #region [Get SteamAccounts]

        /// <summary>
        /// Sends GET request to http://api.steampowered.com/
        /// for steam accounts.
        /// </summary>
        /// <param name="id64">64-bit ids of accounts you want.</param>
        /// <returns>Collection of steam accounts</returns>
        public async Task<AccountCollection> GetSteamAccountsAsync(params string[] id64)
        {
            var uBuilder = new UrlBuilder(GET_STEAM_ACCOUNT_URL,
                new QueryParam("key", DevKey),
                new QueryParam("steamids", string.Join(',', id64)));

            return await RequestAndDeserialize<AccountCollection>(uBuilder.Url, CToken.None);
        }

        /// <summary>
        /// Sends GET request to http://api.steampowered.com/
        /// for steam accounts. Request can be cancelled by providing
        /// cancellation token.
        /// </summary>
        /// <param name="token">cancellation token</param>
        /// <param name="id64">64-bit ids of accounts you want.</param>
        /// <returns>Collection of steam accounts</returns>
        public async Task<AccountCollection> GetSteamAccountsAsync(CToken token, params string[] id64)
        {
            var uBuilder = new UrlBuilder(GET_STEAM_ACCOUNT_URL, 
                new QueryParam("key", DevKey),
                new QueryParam("steamids", string.Join(',', id64)));

            return await RequestAndDeserialize<AccountCollection>(uBuilder.Url, token);
        }

        #endregion
    }
}
