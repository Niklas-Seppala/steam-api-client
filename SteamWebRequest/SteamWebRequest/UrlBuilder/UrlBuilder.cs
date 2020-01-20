using System;
using System.Collections.Specialized;
using System.Web;

namespace SteamWebRequest
{
    /// <summary>
    /// Simpliefies url querystring creation.
    /// </summary>
    public class UrlBuilder
    {
        private UriBuilder _uriBuilder;
        private NameValueCollection _query;

        /// <summary>
        /// Instatiates UrlBuilder object without
        /// querystring (may be added later).
        /// </summary>
        /// <param name="baseUrl">base url</param>
        public UrlBuilder(string baseUrl)
        {
            if (string.IsNullOrEmpty(baseUrl))
            {
                throw (baseUrl == null)
                    ? new ArgumentNullException("Url parameter string can't be null.")
                    : new ArgumentException("Url parameter string can't be empty.");
            }
            else
            {
                _uriBuilder = new UriBuilder(baseUrl);
                _uriBuilder.Port = -1;
                _query = HttpUtility.ParseQueryString(_uriBuilder.Query);
            }
        }

        /// <summary>
        /// Instantiates UrlBuilder object with
        /// querystring.
        /// </summary>
        /// <param name="baseUrl">base url</param>
        /// <param name="queries">params array of QueryParam objects</param>
        public UrlBuilder(string baseUrl, params QueryParam[] queries)
            : this(baseUrl)
        {
            foreach (QueryParam pair in queries)
            {
                _query[pair.Key] = pair.Value;
            }
        }
    }
}
