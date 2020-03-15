using System;
using System.Text;
using System.Web;

namespace SteamApi
{
    /// <summary>
    /// Reuseable class for building up URL from components.
    /// After building up all the components is complete, URL
    /// can be popped out. This applies correct URL encoding and cleans
    /// objects internal state, so builder can be reused in the future.
    /// </summary>
    public class ApiUrlBuilder : UriBuilder
    {
        /// <summary>
        /// Instantiates ApiUrlBuilder object
        /// </summary>
        /// <param name="scheme"></param>
        public ApiUrlBuilder(string scheme = "https")
        {
            Scheme = scheme;
        }

        /// <summary>
        /// Adds subpath to url
        /// </summary>
        /// <param name="subPath">part of the path</param>
        public void AppendPath(params string[] path)
        {
            StringBuilder sb = new StringBuilder(Path);
            foreach (var item in path)
            {
                if (sb[sb.Length-1] != '/')
                    sb.Append("/");
                if (item[0] == '/')
                    sb.Append(item.Substring(1, item.Length-1));
                else
                    sb.Append(item);
            }
            Path = sb.ToString();
        }

        /// <summary>
        /// Adds query to querystring
        /// </summary>
        /// <param name="key">query key</param>
        /// <param name="value">query value</param>
        /// <returns>this object</returns>
        public ApiUrlBuilder AppendQuery(string key, string value)
        {
            if (string.IsNullOrEmpty(Query))
                Query = EndcodeQuery("?", key, value);
            else
                Query += EndcodeQuery("&", key, value);
            return this;
        }

        /// <summary>
        /// Returns encoded url
        /// </summary>
        /// <returns>url</returns>
        public string GetEncodedUrl()
        {
            return Uri.AbsoluteUri;
        }

        /// <summary>
        /// Returns encoded url and clears internal state
        /// </summary>
        /// <param name="clearScheme"></param>
        /// <returns></returns>
        public string PopEncodedUrl(bool clearScheme = true)
        {
            string url = Uri.AbsoluteUri;
            Clear(clearScheme);
            return url;
        }

        /// <summary>
        /// Clears this object's internal state
        /// </summary>
        /// <param name="clearScheme"></param>
        public void Clear(bool clearScheme)
        {
            if (clearScheme)
            {
                Scheme = "";
                Port = -1;
            }
            Host = "";
            Path = "";
            Password = "";
            Query = "";
            Fragment = "";
        }

        /// <summary>
        /// Encodes querystring query
        /// </summary>
        /// <param name="key">query key</param>
        /// <param name="value">query value</param>
        /// <returns>encoded querystring query</returns>
        private string EndcodeQuery(string prefix, string key, string value)
        {
            return $"{prefix}{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(value)}";
        }
    }
}
