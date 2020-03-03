using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace SteamApi.Utility.Url
{
    /// <summary>
    /// This class is made to handle storing and manipulating
    /// url components.
    /// </summary>
    internal class UrlComponents
    {
        /// <summary>
        /// URL schema. Example: "https"
        /// </summary>
        public string Schema { get; set; } = "";

        /// <summary>
        /// URL path. Example: /foo/bar
        /// </summary>
        public string Path { get; set; } = "";

        /// <summary>
        /// URL host. Example: www.google.com
        /// </summary>
        public string Host { get; set; } = "";

        /// <summary>
        /// URL fragment. Example: #footer
        /// </summary>
        public string Fragment { get; set; } = "";

        /// <summary>
        /// URL port. Example: :5050
        /// </summary>
        public int Port { get; set; } = -1;

        /// <summary>
        /// URL querystring. Example: ?key=123&name=nikke
        /// </summary>
        public string Query => _querystring.Count > 0 ? QueriesToString(includeEncoding: false) : "";

        /// <summary>
        /// Main stringbuilder that builds the url from components.
        /// </summary>
        private readonly StringBuilder _mainStrBuilder = new StringBuilder();

        /// <summary>
        /// Secondary stringbuilder that builds individual components.
        /// This way, the main stringbuilder can keep its internal state.
        /// </summary>
        private readonly StringBuilder _helperStrBuilder = new StringBuilder();

        /// <summary>
        /// NameValueCollection of querystring arguments.
        /// </summary>
        private readonly NameValueCollection _querystring = new NameValueCollection();

        /// <summary>
        /// Compiles URL from individual components.
        /// </summary>
        /// <returns>Compelete URL</returns>
        public string CompileUrl()
        {
            if (!string.IsNullOrEmpty(Schema))
            {
                _mainStrBuilder.Append($"{Schema}://");
            }

            _mainStrBuilder.Append(Host);

            if (Port > 0)
            {
                _mainStrBuilder.Append($":{Port}");
            }

            _mainStrBuilder.Append(HttpUtility.UrlPathEncode(Path));

            if (_querystring.Count > 0)
            {
                _mainStrBuilder.Append($"?{QueriesToString(includeEncoding: true)}");
            }

            if (!string.IsNullOrEmpty(Fragment))
            {
                _mainStrBuilder.Append($"#{HttpUtility.UrlEncode(Fragment)}");
            }

            string result = _mainStrBuilder.ToString();
            _mainStrBuilder.Clear(); // clear the main stringbuilder for reusability
            return result;
        }

        /// <summary>
        /// Clears the internal state of UrlComponents object
        /// </summary>
        /// <param name="clearSchema">true if you want schema to be cleared</param>
        public void Clear(bool clearSchema)
        {
            if (clearSchema) Schema = "";
            Port = -1;
            Path = "";
            Host = "";
            Fragment = "";
            _querystring.Clear();
        }

        /// <summary>
        /// Creates Path component
        /// </summary>
        /// <param name="path">path dir array</param>
        public void CreatePath(string[] path)
        {
            if (path.Length > 0)
            {
                foreach (string item in path)
                    _helperStrBuilder.Append(item.StartsWith("/") ? item : $"/{item}");

                Path = _helperStrBuilder.ToString();
                _helperStrBuilder.Clear();
            }
        }

        /// <summary>
        /// Adds query parameter to querystring
        /// </summary>
        /// <param name="key">query param key</param>
        /// <param name="value">query param value</param>
        public void AddQuery(string key, string value)
        {
            _querystring[key] = value;
        }

        /// <summary>
        /// Removes query parameter from querystring
        /// </summary>
        /// <param name="key">query param key</param>
        public void RemoveQuery(string key)
        {
            _querystring.Remove(key);
        }

        /// <summary>
        /// Empties the whole querystring
        /// </summary>
        public void ClearQueries()
        {
            _querystring.Clear();
        }

        /// <summary>
        /// Creates querystring from NameValueCollection.
        /// Applies URL encoding.
        /// </summary>
        /// <returns>querystring as a string</returns>
        private string QueriesToString(bool includeEncoding)
        {
            foreach (var query in GetQueries())
            {
                EncodeQuery(query, includeEncoding, out string key, out string value);
                _helperStrBuilder.Append($"{key}={value}&");
            }
            _helperStrBuilder.Remove(_helperStrBuilder.Length - 1, 1);
            string result = _helperStrBuilder.ToString();
            _helperStrBuilder.Clear();
            return result;
        }

        /// <summary>
        /// Creates collection of name value pairs from querystring collection.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<KeyValuePair<string, string>> GetQueries()
        {
            return _querystring.AllKeys.SelectMany(_querystring.GetValues, (k, v) =>
                new KeyValuePair<string, string>(k, v));
        }

        /// <summary>
        /// Encodes querystring parameter
        /// </summary>
        /// <param name="query">query param as key value pair</param>
        /// <param name="encoding">encoding included</param>
        /// <param name="key">encoded key as out reference</param>
        /// <param name="value">encoded value as out reference</param>
        private void EncodeQuery(KeyValuePair<string, string> query, bool includeEncoding,
            out string key, out string value)
        {
            if (includeEncoding)
            {
                key = HttpUtility.UrlEncode(query.Key);
                value = HttpUtility.UrlEncode(query.Value);
            }
            else // No encoding requested.
            {
                key = query.Key;
                value = query.Value;
            }
        }
    }
}
