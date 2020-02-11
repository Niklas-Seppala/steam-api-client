using System;
using System.Linq;

namespace SteamApi
{
    internal static partial class UrlParser
    {
        public static string ParseSchema(ref ReadOnlySpan<char> url)
        {
            string result = "";
            int start = 0;
            if (UrlContainsSchema(url, out int end))
            {
                var schemaSlice = new UrlSlice(start, end);
                result = CutSlice(url, schemaSlice);
                RemoveSlice(ref url, schemaSlice, endPadding: 3);
            }
            return result;
        }

        public static string ParseHost(ref ReadOnlySpan<char> url)
        {
            var hostSlice = ExtractHostSlice(url);
            string result = CutSlice(url, hostSlice);
            RemoveSlice(ref url, hostSlice);
            return result;
        }

        public static string ParsePort(ref ReadOnlySpan<char> url)
        {
            string result = "";
            if (url.Contains(":".AsSpan(), StringComparison.Ordinal))
            {
                var portSlice = ExtractPortSlice(url);
                result = CutSlice(url, portSlice);
                RemoveSlice(ref url, portSlice, startPadding: 1, endPadding: 1);
            }
            return result;
        }

        public static string ParsePath(ref ReadOnlySpan<char> url)
        {
            string result = "";
            if (url.Contains("/".AsSpan(), StringComparison.Ordinal))
            {
                var pathSlice = ExtractPathSlice(url);
                result = CutSlice(url, pathSlice);
                RemoveSlice(ref url, pathSlice);
            }
            return result;
        }

        public static string ParseQuery(ref ReadOnlySpan<char> url)
        {
            string result = "";
            if (url.Contains("?".AsSpan(), StringComparison.Ordinal))
            {
                UrlSlice querySlice = ExtractQuerySlice(url);
                result = CutSlice(url, querySlice);
                RemoveSlice(ref url, querySlice);
            }
            return result;
        }

        public static string ParseFragment(ref ReadOnlySpan<char> url)
        {
            string result = "";
            if (UrlContainsFragment(url, out int start))
            {
                var fragmentSlice = new UrlSlice(start + 1, url.Length-1);
                result = CutSlice(url, fragmentSlice);
                RemoveSlice(ref url, fragmentSlice, startPadding: 1, endPadding: 1);
            }
            return result;
        }

        readonly struct UrlSlice
        {
            public int Start { get; }
            public int End { get; }
            public int Length => End - Start;

            public UrlSlice(int start, int end)
            {
                Start = start;
                End = end;
            }
        }
    }
}
