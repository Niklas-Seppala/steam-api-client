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
        /// <exception cref="ArgumentNullException">
        /// Thrown when baseUrl parameter is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when baseUrl parameter is empty
        /// </exception>
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
        /// <exception cref="ArgumentNullException">
        /// Thrown when baseUrl parameter is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when baseUrl parameter is empty
        /// </exception>
        public UrlBuilder(string baseUrl, params QueryParam[] queries)
            : this(baseUrl)
        {
            foreach (QueryParam pair in queries)
            {
                _query[pair.Key] = pair.Value;
            }
        }

        /// <summary>
        /// Adds key and value to url querystring.
        /// </summary>
        /// <param name="key">parameter key</param>
        /// <param name="value">parameter value</param>
        public void AddQueryParameter(string key, string value)
        {
            _query[key] = value;
        }

        /// <summary>
        /// Returns string representation of complete url
        /// including possible querystring.
        /// </summary>
        /// <returns>complete url</returns>
        public override string ToString()
        {
            _uriBuilder.Query = _query.ToString();
            return _uriBuilder.ToString();
        }
    }
}
