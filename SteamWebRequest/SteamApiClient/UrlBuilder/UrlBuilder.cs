using System;
using System.Collections.Specialized;
using System.Web;

namespace SteamApiClient
{
    // TODO: Update Tests
    /// <summary>
    /// Simpliefies url querystring creation.
    /// </summary>
    public class UrlBuilder
    {
        private readonly UriBuilder _uriBuilder;
        private readonly NameValueCollection _queryString;

        #region [Properties]
        public string Query => _queryString.ToString();
        public string Host => _uriBuilder.Host;
        public string Url => this.ToString();
        #endregion

        #region [Constructors]

        /// <summary>
        /// Instantiates UrlBuilder object.
        /// </summary>
        public UrlBuilder() { }

        /// <summary>
        /// Instatiates UrlBuilder object without
        /// querystring (may be added later).
        /// </summary>
        /// <param name="url">url</param>
        /// <exception cref="ArgumentNullException">baseUrl parameter is null</exception>
        /// <exception cref="ArgumentException">baseUrl parameter is empty</exception>
        public UrlBuilder(string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw (url == null)
                    ? new ArgumentNullException("Url parameter string can't be null.")
                    : new ArgumentException("Url parameter string can't be empty.");
            }
            else
            {
                _uriBuilder = new UriBuilder(url) { Port = -1};
                _queryString = HttpUtility.ParseQueryString(_uriBuilder.Query);
            }
        }

        /// <summary>
        /// Instantiates UrlBuilder object with
        /// querystring.
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="queries">(string, string) tuple of query parameter</param>
        /// <exception cref="ArgumentException">baseUrl parameter is empty</exception>
        public UrlBuilder(string url, params (string key, string value)[] queries)
            : this(url)
        {
            foreach (var pair in queries)
            {
                _queryString[pair.key] = pair.value;
            }
        }

        #endregion

        #region [Public Methods]

        /// <summary>
        /// Adds key and value to url querystring.
        /// </summary>
        /// <param name="key">parameter key</param>
        /// <param name="value">parameter value</param>
        public void AddQuery((string key, string value) query)
        {
            _queryString[query.key] = query.value;
        }

        /// <summary>
        /// Returns string representation of complete url
        /// including possible querystring.
        /// </summary>
        /// <returns>complete url</returns>
        public override string ToString()
        {
            _uriBuilder.Query = _queryString.ToString();
            return _uriBuilder.ToString();
        }

        #endregion

        #region [Static Methods]

        /// <summary>
        /// Creates url based on steam api schema.
        /// </summary>
        /// <param name="domain">domain name</param>
        /// <param name="iFace">interface name</param>
        /// <param name="method">method name</param>
        /// <param name="version">method version</param>
        /// <returns>complete Steam api url</returns>
        public static string SteamCompleteUrl((string domain, string iFace, string method, string version) comp,
            params (string key, string value)[] queries)
        {
            if (string.IsNullOrEmpty(comp.domain) || string.IsNullOrEmpty(comp.iFace) ||
                string.IsNullOrEmpty(comp.method) || string.IsNullOrEmpty(comp.version))
            {
                throw new ArgumentNullException("Some of the base url components is null or empty");
            }
            else
            {
                string url = SteamBaseUrl(comp);
                var uriB = new UriBuilder(url);

                var querystring = HttpUtility.ParseQueryString(uriB.Query);
                foreach (var (key, value) in queries)
                {
                    querystring[key] = value;
                }

                uriB.Query = querystring.ToString();
                return uriB.ToString();
            }
        }

        /// <summary>
        /// Creates base url string
        /// </summary>
        /// <param name="d">domain</param>
        /// <param name="i">interface</param>
        /// <param name="m">method</param>
        /// <param name="v">version</param>
        /// <returns>url string</returns>
        public static string SteamBaseUrl((string domain, string iFace, string method, string version) url)
        {
            if (url.domain == null || url.iFace == null ||
                url.method == null || url.version == null)
            {
                throw new ArgumentNullException("Some of the base url components is null");
            }
            else
            {
                return $"https://{url.domain}/{url.iFace}/{url.method}/{url.version}/";
            }
        }

        #endregion
    }
}