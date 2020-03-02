using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace SteamApi
{
    internal class UrlComponents
    {
        public string Schema { get; set; } = "";
        public string Path { get; set; } = "";
        public string Host { get; set; } = "";
        public string Fragment { get; set; } = "";
        public int Port { get; set; } = -1;
        public string Query => _querystring.Count > 0 ? QueriesToString(UrlEncoding.None) : "";

        private readonly StringBuilder _strBuilder = new StringBuilder();
        private readonly StringBuilder _helperStrBuilder = new StringBuilder();
        private readonly NameValueCollection _querystring = new NameValueCollection();

        public string CompileUrl()
        {
            if (!string.IsNullOrEmpty(Schema))
            {
                _strBuilder.Append($"{Schema}://");
            }
            _strBuilder.Append(Host);
            if (Port > 0)
            {
                _strBuilder.Append($":{Port}");
            }
            _strBuilder.Append(HttpUtility.UrlPathEncode(Path));
            if (_querystring.Count > 0)
            {
                _strBuilder.Append($"?{QueriesToString(UrlEncoding.Included)}");
            }
            if (!string.IsNullOrEmpty(Fragment))
            {
                _strBuilder.Append($"#{HttpUtility.UrlEncode(Fragment)}");
            }
            string result = _strBuilder.ToString();
            _strBuilder.Clear();
            return result;
        }

        public void Clear(bool clearSchema)
        {
            if (clearSchema)
                Schema = "";
            Port = -1;
            Path = "";
            Host = "";
            Fragment = "";
            _querystring.Clear();
        }

        public void CreatePath(string[] path)
        {
            if (path.Length > 0)
            {
                foreach (string item in path)
                {
                    _helperStrBuilder.Append(item.StartsWith("/") ? item : $"/{item}");
                }
                Path = _helperStrBuilder.ToString();
                _helperStrBuilder.Clear();
            }
        }

        public void AddQuery(string key, string value)
        {
            _querystring[key] = value;
        }

        public void RemoveQuery(string key)
        {
            _querystring.Remove(key);
        }

        public void ClearQueries()
        {
            _querystring.Clear();
        }

        private string QueriesToString(UrlEncoding encoding)
        {
            foreach (var query in GetQueries())
            {
                EncodeQueries(query, encoding, out string key, out string value);
                _helperStrBuilder.Append($"{key}={value}&");
            }
            _helperStrBuilder.Remove(_helperStrBuilder.Length - 1, 1);
            string result = _helperStrBuilder.ToString();
            _helperStrBuilder.Clear();
            return result;
        }

        private IEnumerable<KeyValuePair<string, string>> GetQueries()
        {
            return _querystring.AllKeys.SelectMany(_querystring.GetValues, (k, v) =>
                new KeyValuePair<string, string>(k, v));
        }

        private void EncodeQueries(KeyValuePair<string, string> query, UrlEncoding encoding,
            out string key, out string value)
        {
            if (encoding == UrlEncoding.Included)
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

        private enum UrlEncoding
        {
            None,
            Included
        }
    }
}
