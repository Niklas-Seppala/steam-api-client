using System;
using System.Collections.Specialized;
using System.Web;
using System.Collections.Generic;

namespace SteamApiClient
{
    /// <summary>
    /// Simpliefies url querystring creation.
    /// </summary>
    public class UrlBuilder
    {
        #region [Fields]
        private readonly UriBuilder _uriBuilder;
        private readonly NameValueCollection _query;
        #endregion

        #region [Properties]
        public string Query => _query.ToString();
        public string Host => _uriBuilder.Host;
        public int Port
        {
            get => _uriBuilder.Port;
            set
            {
                if (value < -1)
                {
                    throw new ArgumentOutOfRangeException("Port can't be smaller value than -1");
                }
                else
                {
                    _uriBuilder.Port = value;
                }
            }
        }
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
        /// <exception cref="ArgumentNullException">
        /// Thrown when baseUrl parameter is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when baseUrl parameter is empty
        /// </exception>
        public UrlBuilder(string url, int port = -1)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw (url == null)
                    ? new ArgumentNullException("Url parameter string can't be null.")
                    : new ArgumentException("Url parameter string can't be empty.");
            }
            else
            {
                _uriBuilder = new UriBuilder(url);
                this.Port = port;
                _query = HttpUtility.ParseQueryString(_uriBuilder.Query);
            }
        }

        ///// <summary>
        ///// Instantiates UrlBuilder object with
        ///// querystring.
        ///// </summary>
        ///// <param name="url">url</param>
        ///// <param name="queries">params array of QueryParam objects</param>
        ///// <exception cref="ArgumentNullException">
        ///// Thrown when baseUrl parameter is null.
        ///// </exception>
        ///// <exception cref="ArgumentException">
        ///// Thrown when baseUrl parameter is empty
        ///// </exception>
        //public UrlBuilder(string url, params QueryParam[] queries)
        //    : this(url)
        //{
        //    foreach (QueryParam pair in queries)
        //    {
        //        _query[pair.Key] = pair.Value;
        //    }
        //}

        /// <summary>
        /// Instantiates UrlBuilder object with
        /// querystring.
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="queries">(string, string) tuple of query parameter</param>
        /// <exception cref="ArgumentException">
        /// Thrown when baseUrl parameter is empty
        /// </exception>
        public UrlBuilder(string url, params (string key, string value)[] queries)
            : this(url)
        {
            foreach (var pair in queries)
            {
                _query[pair.key] = pair.value;
            }
        }

        /// <summary>
        /// Instantiates UrlBuilder object with
        /// querystring. 
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="port">port</param>
        /// <param name="queries">params array of QueryParam objects</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when baseUrl parameter is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when baseUrl parameter is empty
        /// </exception>
        public UrlBuilder(string url, int port, params (string key, string value)[] queries)
            : this(url, port)
        {
            foreach (var pair in queries)
            {
                _query[pair.key] = pair.value;
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
            _query[query.key] = query.value;
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

        #endregion

        #region [Non-Public Methods]

        #endregion
    }
}