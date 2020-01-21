using System.Threading.Tasks;
using SteamWebRequest.Models;
//                               !! ALIAS !!
using CToken = System.Threading.CancellationToken;


namespace SteamWebRequest
{
    public static partial class SteamHttpClient
    {
        #region [Base URLs]
        private const string GET_STEAM_ACCOUNT_URL = "http://api.steampowered.com/ISteamUser/GetPlayerSummaries/v0002/";
        #endregion

        #region [GetSteamAccounts]

        /// <summary>
        /// Sends GET request to http://api.steampowered.com/
        /// for steam accounts.
        /// </summary>
        /// <param name="id64">64-bit ids of accounts you want.</param>
        /// <returns>Collection of steam accounts</returns>
        public static async Task<AccountCollection> GetSteamAccountsAsync(params string[] id64)
        {
            var uBuilder = new UrlBuilder(GET_STEAM_ACCOUNT_URL,
                new QueryParam("key", _devKey),
                new QueryParam("steamids", string.Join(',', id64)));

            return await SendGET_AndDeserialize<AccountCollection>(uBuilder.Url, CToken.None);
        }

        /// <summary>
        /// Sends GET request to http://api.steampowered.com/
        /// for steam accounts. Request can be cancelled by providing
        /// cancellation token.
        /// </summary>
        /// <param name="token">cancellation token</param>
        /// <param name="id64">64-bit ids of accounts you want.</param>
        /// <returns>Collection of steam accounts</returns>
        public static async Task<AccountCollection> GetSteamAccountsAsync(CToken token, params string[] id64)
        {
            var uBuilder = new UrlBuilder(GET_STEAM_ACCOUNT_URL, 
                new QueryParam("key", _devKey),
                new QueryParam("steamids", string.Join(',', id64)));

            return await SendGET_AndDeserialize<AccountCollection>(uBuilder.Url, token);
        }

        #endregion
    }
}
