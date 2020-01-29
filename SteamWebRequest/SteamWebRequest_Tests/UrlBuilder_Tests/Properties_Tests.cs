using SteamApiClient;
using System;
using Xunit;

namespace SWR.UrlBuilder_Tests
{
    public class Properties_Tests
    {
        // ------------------------------------------------------------------ \\
        //                         UrlBuilder::Host                           \\
        //                                                                    \\
        // ------------------------ Correct usage --------------------------- \\

        [Theory]
        [InlineData("https://google.com", "google.com")]
        [InlineData("www.reddit.com/r", "www.reddit.com")]
        [InlineData("https://github.com/NikkeArp/steam-api-core", "github.com")]
        public void Property_Host_GetHost_HostReturned(string url, string host)
        {
            var urlBuilder = new UrlBuilder(url);
            Assert.Equal(host, urlBuilder.Host);
        }

        // ------------------------------------------------------------------ \\
        //                         UrlBuilder::Query                          \\
        //                                                                    \\
        // ------------------------ Correct usage --------------------------- \\

        [Theory]
        [InlineData("www.reddit.com/", "")]
        [InlineData("https://github.com/dotnet/corefx/search?q=Cool+stuff&unscoped_q=Cool+stuff",
            "q=Cool+stuff&unscoped_q=Cool+stuff")]
        [InlineData("https://www.youtube.com/results?search_query=.net", "search_query=.net")]
        public void Property_Query_GetQuery_QueryReturned(string url, string query)
        {
            var urlBuilder = new UrlBuilder(url);
            Assert.Equal(query, urlBuilder.Query);
        }

        // ------------------------------------------------------------------ \\
        //                         UrlBuilder::Port                           \\
        //                                                                    \\
        // ------------------------ Correct usage --------------------------- \\

        [Theory]
        [InlineData(5000, 5000)]
        [InlineData(8080, 8080)]
        [InlineData(0, 0)]
        public void Property_Port_GetPort_ReturnPort(int portSetup, int port)
        {
            var urlBuilder = new UrlBuilder("https://github.com", port: portSetup);
            Assert.Equal(port, urlBuilder.Port);
        }

        // --------------------------- Wrong usage -------------------------- \\

        [Fact]
        public void Property_Port_SetPort_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var urlBuilder = new UrlBuilder("https://github.com");
                urlBuilder.Port = -50;
            });
        }

        // ------------------------------------------------------------------ \\
        //                         UrlBuilder::Url                            \\
        //                                                                    \\
        // ------------------------ Correct usage --------------------------- \\

        [Theory]
        [InlineData("https://google.com/", "https://google.com/?key=3278382GH&name=Joe")]
        [InlineData("http://reddit.com/r/", "http://reddit.com/r/?key=3278382GH&name=Joe")]
        [InlineData("https://github.com/NikkeArp/", "https://github.com/NikkeArp/?key=3278382GH&name=Joe")]
        public void Property_Url_GetUrl_ReturnUrl(string url, string fullUrl)
        {
            var urlBuilder = new UrlBuilder(url,
                ("key", "3278382GH"),
                ("name", "Joe"));

            Assert.Equal(fullUrl, urlBuilder.Url);
        }
    }
}
