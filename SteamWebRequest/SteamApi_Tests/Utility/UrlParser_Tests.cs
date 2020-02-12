using SteamApi;
using System;
using Xunit;

namespace Utility
{
    public class UrlParser_Tests
    {
        [Theory]
        [InlineData("https://www.google.com", "https")]
        [InlineData("ftp://nikke@host/foo/bar", "ftp")]
        [InlineData("http://www.google.com", "http")]
        [InlineData("telnet://192.0.2.16:80/", "telnet")]
        public void ParseSchema_ValidUrlSpan_ReturnsParsedSchema(string url, string correctSchema)
        {
            ReadOnlySpan<char> urlSpan = url;
            string result = UrlParser.ParseSchema(ref urlSpan);

            Assert.Equal(correctSchema, result);
        }

        [Theory]
        [InlineData("www.google.com")]
        [InlineData("nikke@gmail.com")]
        [InlineData("/foo/bar/pic.png")]
        public void ParseSchema_NoSchemaInUrlSpan_ReturnsEmptyString(string url)
        {
            ReadOnlySpan<char> urlSpan = url;
            string result = UrlParser.ParseSchema(ref urlSpan);

            Assert.Empty(result);
        }

        [Theory]
        [InlineData("https://www.google.com", "www.google.com")]
        [InlineData("ftp://nikke@host/foo/bar", "nikke@host/foo/bar")]
        [InlineData("http://www.google.com", "www.google.com")]
        [InlineData("telnet://192.0.2.16:80/", "192.0.2.16:80/")]
        public void ParseSchema_ValidUrlSpan_SchemaParsedFromSpan(string url, string afterOp)
        {
            ReadOnlySpan<char> urlSpan = url;
            _ = UrlParser.ParseSchema(ref urlSpan);

            Assert.Equal(afterOp, urlSpan.ToString());
        }


        [Theory]
        [InlineData("www.google.com", "www.google.com")]
        [InlineData("nikke@host/foo/bar", "nikke@host")]
        [InlineData("192.0.2.16:80/", "192.0.2.16")]
        public void ParseHost_UrlParsedToHost_ReturnsHost(string url, string correctHost)
        {
            ReadOnlySpan<char> urlSpan = url;
            string result = UrlParser.ParseHost(ref urlSpan);

            Assert.Equal(correctHost, result);
        }

        [Theory]
        [InlineData("?jee=joo#dahds")]
        [InlineData("/foo/bar?key=jee")]
        [InlineData(":80/foo/bar/bor/bör")]
        public void ParseHost_NoHostInUrl_ReturnsEmptyString(string url)
        {
            ReadOnlySpan<char> urlSpan = url;
            string result = UrlParser.ParseHost(ref urlSpan);

            Assert.Empty(result);
        }

        [Theory]
        [InlineData("www.google.com", "")]
        [InlineData("nikke@host/foo/bar", "/foo/bar")]
        [InlineData("192.0.2.16:80/", ":80/")]
        public void ParseHost_ValidUrlSpan_HostParsedFromSpan(string url, string afterOp)
        {
            ReadOnlySpan<char> urlSpan = url;
            _ = UrlParser.ParseHost(ref urlSpan);

            Assert.Equal(afterOp, urlSpan.ToString());
        }

        [Theory]
        [InlineData(":8080?key=1234", "8080")]
        [InlineData(":66/foo/bar", "66")]
        [InlineData(":80/", "80")]
        [InlineData(":80", "80")]
        public void ParsePort_UrlParsedToPort_ReturnsPort(string url, string correctPort)
        {
            ReadOnlySpan<char> urlSpan = url;
            string result = UrlParser.ParsePort(ref urlSpan);

            Assert.Equal(correctPort, result);
        }

        [Theory]
        [InlineData("?jee=joo#dahds")]
        [InlineData("/foo/bar?key=jee")]
        [InlineData("this_is_not_a_port")]
        public void ParsePort_NoHostInUrl_ReturnsEmptyString(string url)
        {
            ReadOnlySpan<char> urlSpan = url;
            string result = UrlParser.ParsePort(ref urlSpan);

            Assert.Empty(result);
        }

        [Theory]
        [InlineData(":8080?key=1234", "?key=1234")]
        [InlineData(":66/foo/bar", "/foo/bar")]
        [InlineData(":80/", "/")]
        public void ParsePort_ValidUrlSpan_PortParsedFromSpan(string url, string afterOp)
        {
            ReadOnlySpan<char> urlSpan = url;
            _ = UrlParser.ParsePort(ref urlSpan);

            Assert.Equal(afterOp, urlSpan.ToString());
        }

        [Theory]
        [InlineData("/foo/bar/boor", "/foo/bar/boor")]
        [InlineData("/foo/bar?key=123", "/foo/bar")]
        [InlineData("/foo/bar/?key=123", "/foo/bar/")]
        [InlineData("/foo/bar#frag", "/foo/bar")]
        public void ParsePath_UrlParsedToPort_ReturnsPath(string url, string correctPath)
        {
            ReadOnlySpan<char> urlSpan = url;
            string result = UrlParser.ParsePath(ref urlSpan);

            Assert.Equal(correctPath, result);
        }

        [Theory]
        [InlineData("?key=09878")]
        [InlineData("")]
        [InlineData("#frag")]
        public void ParsePath_NoPathInUrl_ReturnsEmptyString(string url)
        {
            ReadOnlySpan<char> urlSpan = url;
            string result = UrlParser.ParsePath(ref urlSpan);

            Assert.Empty(result);
        }

        [Theory]
        [InlineData("/foo/bar/boor", "")]
        [InlineData("/foo/bar?key=123", "?key=123")]
        [InlineData("/foo/bar/?key=123", "?key=123")]
        [InlineData("/foo/bar#frag", "#frag")]
        public void ParsePath_ValidUrlSpan_PathParsedFromSpan(string url, string afterOp)
        {
            ReadOnlySpan<char> urlSpan = url;
            _ = UrlParser.ParsePath(ref urlSpan);

            Assert.Equal(afterOp, urlSpan.ToString());
        }

        [Theory]
        [InlineData("?key=123", "?key=123")]
        [InlineData("?key=123&asd", "?key=123&asd")]
        [InlineData("?q=50&key=567&foo=bar#frag", "?q=50&key=567&foo=bar")]
        public void ParseQuery_UrlParsedToPath_ReturnsQuery(string url, string correctQuery)
        {
            ReadOnlySpan<char> urlSpan = url;
            string result = UrlParser.ParseQuery(ref urlSpan);

            Assert.Equal(correctQuery, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("#frag")]
        public void ParseQuery_NoQueryInUrl_ReturnsEmptyString(string url)
        {
            ReadOnlySpan<char> urlSpan = url;
            string result = UrlParser.ParseQuery(ref urlSpan);

            Assert.Empty(result);
        }

        [Theory]
        [InlineData("?key=123", "")]
        [InlineData("?key=123&asd", "")]
        [InlineData("?q=50&key=567&foo=bar#frag", "#frag")]
        public void ParseQuery_ValidUrlSpan_QueryParsedFromSpan(string url, string afterOp)
        {
            ReadOnlySpan<char> urlSpan = url;
            _ = UrlParser.ParseQuery(ref urlSpan);

            Assert.Equal(afterOp, urlSpan.ToString());
        }

        [Theory]
        [InlineData("#diipa_daapa", "diipa_daapa")]
        [InlineData("#frag", "frag")]
        public void ParseFragment_UrlParsedToPath_ReturnsQuery(string url, string correctFragment)
        {
            ReadOnlySpan<char> urlSpan = url;
            string result = UrlParser.ParseFragment(ref urlSpan);

            Assert.Equal(correctFragment, result);
        }

        [Theory]
        [InlineData("")]
        [InlineData("not a fragment")]
        public void ParseFragment_NoFragmentInUrl_ReturnsEmptyString(string url)
        {
            ReadOnlySpan<char> urlSpan = url;
            string result = UrlParser.ParseFragment(ref urlSpan);

            Assert.Empty(result);
        }

        [Theory]
        [InlineData("#diipa_daapa", "")]
        [InlineData("#frag", "")]
        public void ParseFragment_ValidUrlSpan_FragmentParsedFromSpan(string url, string afterOp)
        {
            ReadOnlySpan<char> urlSpan = url;
            _ = UrlParser.ParseFragment(ref urlSpan);

            Assert.Equal(afterOp, urlSpan.ToString());
        }
    }
}
