using System;
using System.Linq;
using System.Web;
using SteamApi.Utility.Url;

namespace SteamApi.Utility
{
    /// <summary>
    /// 
    /// </summary>
    internal class UrlBuilder
    {
        public string Url => ToString();
        public UrlComponents Components { get; } = new UrlComponents();

        public UrlBuilder() {}
        public UrlBuilder(string url) : this()
        {
            ParseUrl(url.AsSpan());
        }

        public UrlBuilder UrlFromString(string url)
        {
            Clear();
            ParseUrl(url.AsSpan());
            return this;
        }

        private void ParseUrl(ReadOnlySpan<char> url)
        {
            Components.Schema = UrlParser.ParseSchema(ref url);
            Components.Host = UrlParser.ParseHost(ref url);
            if (int.TryParse(UrlParser.ParsePort(ref url), out int port))
            {
                Components.Port = port;
            }
            if (!Components.Host.Contains('.') && Components.Port < 0)
            {   // if host doesnt contain '.' or url doesnt have port its invalid
                Clear();
                return;
            }
            Components.Path = UrlParser.ParsePath(ref url);
            var queryCollection = HttpUtility.ParseQueryString(UrlParser.ParseQuery(ref url));
            foreach (string key in queryCollection.AllKeys)
            {
                Components.AddQuery(key, queryCollection[key]);
            }
            Components.Fragment = UrlParser.ParseFragment(ref url);
        }

        public UrlBuilder SetHost(string host)
        {
            Components.Host = host;
            return this;
        }

        public UrlBuilder SetPort(int port)
        {
            Components.Port = port;
            return this;
        }

        public UrlBuilder SetSchema(string schema)
        {
            Components.Schema = schema;
            return this;
        }

        public UrlBuilder SetFragment(string fractal)
        {
            Components.Fragment = fractal;
            return this;
        }

        public UrlBuilder SetPath(params string[] path)
        {
            Components.CreatePath(path);
            return this;
        }

        public UrlBuilder AddQuery(string key, string value)
        {
            Components.AddQuery(key, value);
            return this;
        }

        public UrlBuilder RemoveQuery(string key)
        {
            Components.RemoveQuery(key);
            return this;
        }

        public UrlBuilder ClearQueries()
        {
            Components.ClearQueries();
            return this;
        }

        public string PopUrl()
        {
            string url = Url;
            Clear();
            return url;
        }

        public void Clear(bool clearSchema = false)
        {
            Components.Clear(clearSchema);
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Components.Host))
                return "";
            else
                return Components.CompileUrl();
        }
    }
}
