using System;
using System.Linq;

namespace SteamApi.Utility.Url
{
    /// <summary>
    /// Url parsing class. Provides static methods to
    /// parse components from complete url. Different url components
    /// must be parsed in the correct order, for the methods in this
    /// class consume the target component at the start of the url
    /// being parsed.
    /// </summary>
    internal static class UrlParser
    {
        /// <summary>
        /// Consumes and returns the schema from the start
        /// of the span of chars.
        /// </summary>
        /// <param name="url">url as span</param>
        /// <returns>schema</returns>
        public static string ParseSchema(ref ReadOnlySpan<char> url)
        {
            string result = "";
            int start = 0;
            if (ContainsSchema(url, out int end))
            {
                var schemaSlice = new UrlSlice(start, end);
                result = ExtractSlice(url, schemaSlice);
                RemoveSlice(ref url, schemaSlice, endPadding: 3);
            }
            return result;
        }

        /// <summary>
        /// Consumes and returns the host from the start
        /// of the span of chars.
        /// </summary>
        /// <param name="url">url as span</param>
        /// <returns>host</returns>
        public static string ParseHost(ref ReadOnlySpan<char> url)
        {
            var hostSlice = ExtractHostSlice(url);
            string result = ExtractSlice(url, hostSlice);
            RemoveSlice(ref url, hostSlice);
            return result;
        }

        /// <summary>
        /// Consumes and returns the port from the start
        /// of the span of chars.
        /// </summary>
        /// <param name="url">url as span</param>
        /// <returns>port</returns>
        public static string ParsePort(ref ReadOnlySpan<char> url)
        {
            string result = "";
            if (url.Contains(":".AsSpan(), StringComparison.Ordinal))
            {
                var portSlice = ExtractPortSlice(url);
                result = ExtractSlice(url, portSlice);
                RemoveSlice(ref url, portSlice, startPadding: 1, endPadding: 1);
            }
            return result;
        }

        /// <summary>
        /// Consumes and returns the path from the start
        /// of the span of chars.
        /// </summary>
        /// <param name="url">url as span</param>
        /// <returns>path</returns>
        public static string ParsePath(ref ReadOnlySpan<char> url)
        {
            string result = "";
            if (url.Contains("/".AsSpan(), StringComparison.Ordinal))
            {
                var pathSlice = ExtractPathSlice(url);
                result = ExtractSlice(url, pathSlice);
                RemoveSlice(ref url, pathSlice);
            }
            return result;
        }

        /// <summary>
        /// Consumes and returns the querystring from the start
        /// of the span of chars.
        /// </summary>
        /// <param name="url">url as span</param>
        /// <returns>querystring</returns>
        public static string ParseQuery(ref ReadOnlySpan<char> url)
        {
            string result = "";
            if (url.Contains("?".AsSpan(), StringComparison.Ordinal))
            {
                UrlSlice querySlice = ExtractQuerySlice(url);
                result = ExtractSlice(url, querySlice);
                RemoveSlice(ref url, querySlice);
            }
            return result;
        }

        /// <summary>
        /// Consumes and returns the fragment from the start
        /// of the span of chars.
        /// </summary>
        /// <param name="url">url as span</param>
        /// <returns>fragment</returns>
        public static string ParseFragment(ref ReadOnlySpan<char> url)
        {
            string result = "";
            if (ContainsFragment(url, out int start))
            {
                var fragmentSlice = new UrlSlice(start + 1, url.Length-1);
                result = ExtractSlice(url, fragmentSlice);
                RemoveSlice(ref url, fragmentSlice, startPadding: 1, endPadding: 1);
            }
            return result;
        }

        /// <summary>
        /// Extracts path slice from url
        /// </summary>
        /// <param name="url">url as span</param>
        /// <returns>path</returns>
        private static UrlSlice ExtractPathSlice(ReadOnlySpan<char> url)
        {
            // Checks if url contains more components and if it does, figures the end index of the path component.
            // Otherwise path start index is 0 and end is the remaining length of the url.
            bool containsMore = ContainsQuery(url, out int end) || ContainsFragment(url, out end);
            return containsMore ? new UrlSlice(0, end) : new UrlSlice(0, url.Length);
        }

        /// <summary>
        /// Extracts port slice from url
        /// </summary>
        /// <param name="url">url as span</param>
        /// <returns>port</returns>
        private static UrlSlice ExtractPortSlice(ReadOnlySpan<char> url)
        {
            // See "ExtractPathSlice" comments.
            bool containsMore = ContainsPath(url, out int end) || ContainsQuery(url, out end) || ContainsFragment(url, out end);
            return containsMore ? new UrlSlice(1, end - 1) : new UrlSlice(1, url.Length - 1);
        }

        /// <summary>
        /// Extracts host slice from the url
        /// </summary>
        /// <param name="url">url as span</param>
        /// <returns>host</returns>
        private static UrlSlice ExtractHostSlice(ReadOnlySpan<char> url)
        {
            // See "ExtractPathSlice" comments.
            bool containsMore = ContainsPort(url, out int end) || ContainsPath(url, out end) || ContainsQuery(url, out end) || ContainsFragment(url, out end);
            return containsMore ? new UrlSlice(0, end) : new UrlSlice(0, url.Length);
        }

        /// <summary>
        /// Extracts querystring slice from the url
        /// </summary>
        /// <param name="url">url as span</param>
        /// <returns>querystring</returns>
        private static UrlSlice ExtractQuerySlice(ReadOnlySpan<char> url)
        {
            // See "ExtractPathSlice" comments.
            return ContainsFragment(url, out int end) ? new UrlSlice(0, end) : new UrlSlice(0, url.Length);
        }

        /// <summary>
        /// Removes slice from the url
        /// </summary>
        /// <param name="url">url as span</param>
        /// <param name="removedSlice">slice to be removed</param>
        /// <param name="startPadding">number of chars to skip</param>
        /// <param name="endPadding">number of chars to skip</param>
        private static void RemoveSlice(ref ReadOnlySpan<char> url, UrlSlice removedSlice, int startPadding = 0, int endPadding = 0)
        {
            url = url.Slice(startPadding + removedSlice.Length + endPadding);
        }

        /// <summary>
        /// Returns slice but does not affect
        /// the original url.
        /// </summary>
        /// <param name="url">url as span</param>
        /// <param name="slice">Slice to be extracted</param>
        /// <returns>slice of the url</returns>
        private static string ExtractSlice(in ReadOnlySpan<char> url, UrlSlice slice)
        {
            return url.Slice(slice.Start, slice.End).ToString();
        }

        /// <summary>
        /// Checks if remaining url contains fragment component.
        /// </summary>
        /// <param name="url">remaining url</param>
        /// <param name="index">
        /// Start index of the possible fragment component.
        /// If fragment is missing index is -1 
        /// </param>
        /// <returns>True if url contains fragment</returns>
        private static bool ContainsFragment(ReadOnlySpan<char> url, out int index)
        {
            index = url.IndexOf('#'); // if index is not found, defaults to -1
            return index >= 0 ? true : false;
        }

        /// <summary>
        /// Checks if url contains schema component.
        /// </summary>
        /// <param name="url">url</param>
        /// <param name="index">
        /// End index of the possible schema component.
        /// If schema is missing index is -1 
        /// </param>
        /// <returns>True if url contains schema</returns>
        private static bool ContainsSchema(ReadOnlySpan<char> url, out int index)
        {
            index = url.IndexOf("://".AsSpan()); // if index is not found, defaults to -1
            return index >= 0 ? true : false;
        }

        /// <summary>
        /// Checks if remaining url contains querystring component
        /// </summary>
        /// <param name="url">remaining url</param>
        /// <param name="index">
        /// Start index of the possible querystring component.
        /// If querystring is missing index is -1 
        /// </param>
        /// <returns>True if url contains querystring</returns>
        private static bool ContainsQuery(ReadOnlySpan<char> url, out int index)
        {
            index = url.IndexOf('?'); // if index is not found, defaults to -1
            return index >= 0 ? true : false;
        }

        /// <summary>
        /// Checks if remaining url contains path component
        /// </summary>
        /// <param name="url">remaining url</param>
        /// <param name="index">
        /// Start index of the possible path component.
        /// If path is missing index is -1 
        /// </param>
        /// <returns>True if url contains path</returns>
        private static bool ContainsPath(ReadOnlySpan<char> url, out int index)
        {
            index = url.IndexOf('/'); // if index is not found, defaults to -1
            return index >= 0 ? true : false;
        }

        /// <summary>
        /// Checks if remaining url contains port component
        /// </summary>
        /// <param name="url">remaining url</param>
        /// <param name="index">
        /// Start index of the possible port component.
        /// If port is missing index is -1 
        /// </param>
        /// <returns>True if url contains port</returns>
        private static bool ContainsPort(ReadOnlySpan<char> url, out int index)
        {
            index = url.IndexOf(':'); // if index is not found, defaults to -1
            return index >= 0 ? true : false;
        }
    }
}
