using System;
using System.Collections.Specialized;
using SteamWebRequest;
using Xunit;
using System.Reflection;

namespace SWR.UrlBuilder_Tests
{
    public class Ctor_Tests
    {
        // ------------------------------------------------------------------ \\
        //              UrlBuilder::ctor(string url, int port=-1)             \\
        //                                                                    \\
        // --------------------------- Wrong usage -------------------------- \\

        [Fact]
        public void Url_Port_InvalidInputEmptyString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new UrlBuilder(string.Empty, port: 10));
        }

        [Fact]
        public void Url_Port_InvalidInputNullParam_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UrlBuilder(null, port: -1));
        }

        [Fact]
        public void Url_Port_InvalidPort_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new UrlBuilder("https://google.com", port: -2));
        }

        // -------------------------- Correct usage ------------------------- \\

        [Fact]
        public void Url_Port_ValidInput_ObjectCreated()
        {
            UrlBuilder url = new UrlBuilder("https://google.com/");

            UriBuilder builder = (UriBuilder)typeof(UrlBuilder)
                .GetField("_uriBuilder", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(url);

            NameValueCollection query = (NameValueCollection)typeof(UrlBuilder)
                .GetField("_query", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(url);

            Assert.NotNull(builder);
            Assert.NotNull(query);
        }


        // ---------------------------------------------------------------------- \\
        // UrlBuilder::ctor(string url, int port=-1, params QueryParam[] queries) \\
        //                                                                        \\
        // ----------------------------- Wrong usage ---------------------------- \\

        [Fact]
        public void Url_Port_Queries_InvalidInputEmptyString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new UrlBuilder(
                string.Empty, port: 10,
                new QueryParam("name", "John"),
                new QueryParam("key", "864634DA3")));
        }

        [Fact]
        public void Url_Port_Queries_InvalidInputNullParam_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UrlBuilder(
                null, port: 10,
                new QueryParam("name", "John"),
                new QueryParam("key", "864634DA3")));
        }

        // -------------------------- Correct usage ------------------------- \\
        [Fact]
        public void Url_Port_Queries_ValidInput_ObjectCreated()
        {
            UrlBuilder url = new UrlBuilder(
                "https://google.com/", port: -1,
                new QueryParam("name", "John"),
                new QueryParam("key", "864634DA3"));

            UriBuilder builder = (UriBuilder)typeof(UrlBuilder)
                .GetField("_uriBuilder", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(url);

            NameValueCollection query = (NameValueCollection)typeof(UrlBuilder)
                .GetField("_query", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(url);

            Assert.NotNull(builder);
            Assert.NotNull(query);
        }
    }
}
