using System.Threading.Tasks;
using SteamApi.Models.CsGo;
using Ctoken = System.Threading.CancellationToken;

namespace SteamApi
{
    /// <summary>
    /// Specialized HttpClient for making requests for Valve's CSGO
    /// APIs. Derived from ApiClient class.
    /// </summary>
    public class CsGoClient : ApiClient
    {
        private const string HOST = "api.steampowered.com";

        public CsGoClient(bool testConnection = false, string schema = "https") : base(testConnection, schema)
        {
            // Initialization happens in parent class's constructor.
        }

        /// <summary>
        /// CsGoClient's URL to send test request.
        /// </summary>
        protected override string TestUrl => $"https://api.steampowered.com/ICSGOServers_730/GetGameServersStatus/v1/?key={ApiKey}";


        /// <summary>
        /// Sends http GET request to api.steampowered.com for
        /// CSGO server status info. Request can be cancelled by
        /// providing cancellation token.
        /// </summary>
        /// <param name="version">API method version</param>
        /// <param name="cToken">Cancellation token</param>
        /// <returns>CSGO server status model</returns>
        public async Task<CsGoServerStatus> GetCsGoServerStatusAsync(string version = "v1",
            Ctoken cToken = default)
        {
            UrlBuilder.Host = HOST;
            UrlBuilder.AppendPath("ICSGOServers_730", "GetGameServersStatus", version);
            UrlBuilder.AppendQuery("key", ApiKey);

            var response = await GetModelAsync<CsGoServerStatusResponse>(cToken: cToken)
                .ConfigureAwait(false);

            return response.Result;
        }
    }
}
