using SteamApiClient;
using Xunit;

namespace SWR.UrlBuilder_Tests
{
    public class PublicMethods_Tests
    {
        // ------------------------------------------------------------------ \\
        //                          UrlBuilder::ToString()                    \\
        //                                                                    \\
        // -------------------------- Correct usage ------------------------- \\

        [Theory]
        [InlineData("https://google.com/", "https://google.com/?key=3278382GH&name=Joe")]
        [InlineData("http://reddit.com/r/", "http://reddit.com/r/?key=3278382GH&name=Joe")]
        [InlineData("https://github.com/NikkeArp/", "https://github.com/NikkeArp/?key=3278382GH&name=Joe")]
        public void ToString_ReturnsUrl(string url, string fullUrl)
        {
            var urlBuilder = new UrlBuilder(url,
                ("key", "3278382GH"),
                ("name", "Joe"));

            Assert.Equal(fullUrl, urlBuilder.Url);
        }

        // ------------------------------------------------------------------ \\
        //                          UrlBuilder::AddQuery()                    \\
        //                                                                    \\
        // -------------------------- Correct usage ------------------------- \\

        [Fact]
        public void AddQuery_AddQueryParamer_BuildsCorrectQueryString()
        {
            var urlBuilder = new UrlBuilder("https://google.com");
            urlBuilder.AddQuery(("key", "8730"));
            urlBuilder.AddQuery(("name", "Nikke"));
            urlBuilder.AddQuery(("pet", "none"));

            Assert.Equal("key=8730&name=Nikke&pet=none", urlBuilder.Query);
        }
    }
}
