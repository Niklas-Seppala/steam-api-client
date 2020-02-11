using System;

namespace SteamApi
{
    internal static partial class UrlParser
    {
        private static UrlSlice ExtractPathSlice(ReadOnlySpan<char> url)
        {
            bool containsOtherComponents = UrlContainsQuerystring(url, out int end) 
                                        || UrlContainsFragment(url, out end);
            return containsOtherComponents ? new UrlSlice(0, end) : new UrlSlice(0, url.Length);
        }

        private static UrlSlice ExtractPortSlice(ReadOnlySpan<char> url)
        {
            int end;
            bool containsOtherComponents = UrlContainsPath(url, out end)
                                        || UrlContainsQuerystring(url, out end)
                                        || UrlContainsFragment(url, out end);
            return containsOtherComponents ? new UrlSlice(1, end - 1) : new UrlSlice(1, url.Length - 1);
        }

        private static UrlSlice ExtractHostSlice(ReadOnlySpan<char> url)
        {
            int end;
            bool containsOtherComponents = UrlContainsPort(url, out end)
                                        || UrlContainsPath(url, out end)
                                        || UrlContainsQuerystring(url, out end)
                                        || UrlContainsFragment(url, out end);
            return containsOtherComponents ? new UrlSlice(0, end) : new UrlSlice(0, url.Length);
        }

        private static UrlSlice ExtractQuerySlice(ReadOnlySpan<char> url)
        {
            return UrlContainsFragment(url, out int end) ? new UrlSlice(0, end) : new UrlSlice(0, url.Length);
        }

        private static void RemoveSlice(ref ReadOnlySpan<char> url,
            UrlSlice removedSlice, int startPadding = 0, int endPadding = 0)
        {
            url = url.Slice(startPadding + removedSlice.Length + endPadding);
        }
        private static string CutSlice(in ReadOnlySpan<char> url, UrlSlice slice)
        {
            return url.Slice(slice.Start, slice.End).ToString();
        }
        private static bool UrlContainsFragment(ReadOnlySpan<char> url, out int index)
        {
            index = url.IndexOf('#');
            return index >= 0 ? true : false;
        }
        private static bool UrlContainsSchema(ReadOnlySpan<char> url, out int index)
        {
            index = url.IndexOf("://".AsSpan());
            return index >= 0 ? true : false;
        }
        private static bool UrlContainsQuerystring(ReadOnlySpan<char> url, out int index)
        {
            index = url.IndexOf('?');
            return index >= 0 ? true : false;
        }
        private static bool UrlContainsPath(ReadOnlySpan<char> url, out int index)
        {
            index = url.IndexOf('/');
            return index >= 0 ? true : false;
        }
        private static bool UrlContainsPort(ReadOnlySpan<char> url, out int index)
        {
            index = url.IndexOf(':');
            return index >= 0 ? true : false;
        }
    }
}
