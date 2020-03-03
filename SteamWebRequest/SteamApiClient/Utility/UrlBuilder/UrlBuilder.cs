using System;
using System.Linq;
using System.Web;
using SteamApi.Utility.Url;

namespace SteamApi.Utility
{
    /// <summary>
    /// Reuseable class for URL building from various components.
    /// Does support complete or partial URLs, or nothing at all
    /// for starting point. After that, single components can be
    /// added or replaced. URL components are stored to objects internal
    /// state.
    /// 
    /// After building up all the components is complete, URL
    /// can be popped out. This applies correct URL encoding and cleans
    /// objects internal state, so same object can be reused in the future.
    /// 
    /// If you wish to get URL ready for use, but wish it to remain
    /// in objects internal state for further purposes, use Url-property
    /// or simply ToString() method.
    /// </summary>
    internal class UrlBuilder
    {
        /// <summary>
        /// Get URL as string.
        /// </summary>
        public string Url => ToString();

        /// <summary>
        /// URL component container object.
        /// </summary>
        public UrlComponents Components { get; } = new UrlComponents();

        public UrlBuilder() {}

        /// <summary>
        /// Instantiates UrlBuilder instance from url string.
        /// </summary>
        /// <param name="url">Url string. Must be valid URL</param>
        public UrlBuilder(string url) : this()
        {
            ParseUrl(url.AsSpan());
        }

        /// <summary>
        /// Clears current state and creates new URL
        /// components from provided URL string.
        /// </summary>
        /// <param name="url">URL string. Must be valid URL</param>
        /// <returns>this</returns>
        public UrlBuilder UrlFromString(string url)
        {
            Clear();
            ParseUrl(url.AsSpan());
            return this;
        }

        /// <summary>
        /// Parses URL string to components and storest them
        /// to objects state.
        /// </summary>
        /// <param name="url">URL string as span</param>
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

        /// <summary>
        /// Change current host component to specified new host.
        /// </summary>
        /// <param name="host">New host component</param>
        /// <returns>this</returns>
        public UrlBuilder SetHost(string host)
        {
            Components.Host = host;
            return this;
        }

        /// <summary>
        /// Changes current port component to specified
        /// new port.
        /// </summary>
        /// <param name="port">new port component</param>
        /// <returns>this</returns>
        public UrlBuilder SetPort(int port)
        {
            Components.Port = port;
            return this;
        }

        /// <summary>
        /// Changes current schema component to specified
        /// new schema.
        /// </summary>
        /// <param name="schema">new schema component</param>
        /// <returns>this</returns>
        public UrlBuilder SetSchema(string schema)
        {
            Components.Schema = schema;
            return this;
        }

        /// <summary>
        /// Changes fragment component to specified new
        /// fragment.
        /// </summary>
        /// <param name="fragment">new fragment component</param>
        /// <returns>this</returns>
        public UrlBuilder SetFragment(string fragment)
        {
            Components.Fragment = fragment;
            return this;
        }

        /// <summary>
        /// Changes path component to specified new
        /// path.
        /// </summary>
        /// <param name="path">new path component</param>
        /// <returns>this</returns>
        public UrlBuilder SetPath(params string[] path)
        {
            Components.CreatePath(path);
            return this;
        }

        /// <summary>
        /// Adds new querystring parameter to existing querystring.
        /// </summary>
        /// <param name="key">querystring parameter key</param>
        /// <param name="value">querystring parameter value</param>
        /// <returns>this</returns>
        public UrlBuilder AddQuery(string key, string value)
        {
            Components.AddQuery(key, value);
            return this;
        }

        /// <summary>
        /// Removes querystring parameter from existing querystring.
        /// </summary>
        /// <param name="key">querystring parameter key</param>
        /// <returns>this</returns>
        public UrlBuilder RemoveQuery(string key)
        {
            Components.RemoveQuery(key);
            return this;
        }

        /// <summary>
        /// Clears current querystring completely.
        /// </summary>
        /// <returns>this</returns>
        public UrlBuilder ClearQueries()
        {
            Components.ClearQueries();
            return this;
        }

        /// <summary>
        /// Pops URL from this object. URL is URL encoded
        /// and ready to use. Clears objects internal state,
        /// so it is ready for new URL.
        /// </summary>
        /// <returns>Ready URL string</returns>
        public string PopUrl()
        {
            string url = Url;
            Clear();
            return url;
        }

        /// <summary>
        /// Clears objects internal state. Schema
        /// component is left untouched by default!
        /// </summary>
        /// <param name="clearSchema">clear schema also?</param>
        public void Clear(bool clearSchema = false)
        {
            Components.Clear(clearSchema);
        }

        /// <summary>
        /// Returns URL string encoded and ready for use.
        /// Does not effect objects internal state.
        /// </summary>
        /// <returns>Ready URL string</returns>
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Components.Host))
                return "";
            else
                return Components.CompileUrl();
        }
    }
}
