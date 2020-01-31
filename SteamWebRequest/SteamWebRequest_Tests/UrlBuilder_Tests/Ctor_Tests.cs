using SteamApiClient;
using System;
using System.Collections.Specialized;
using System.Reflection;
using Xunit;

namespace SWR.UrlBuilder_Tests
{
    public class Ctor_Tests
    {
        [Fact]
        public void Url_Port_InvalidInputEmptyString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new UrlBuilder(string.Empty));
        }

        [Fact]
        public void Url_Port_InvalidInputNullParam_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UrlBuilder(null));
        }

        [Fact]
        public void Url_Port_ValidInput_ObjectCreated()
        {
            UrlBuilder url = new UrlBuilder("https://google.com/");

            UriBuilder builder = (UriBuilder)typeof(UrlBuilder)
                .GetField("_uriBuilder", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(url);

            NameValueCollection query = (NameValueCollection)typeof(UrlBuilder)
                .GetField("_queryString", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(url);

            Assert.NotNull(builder);
            Assert.NotNull(query);
        }

        [Fact]
        public void Url_Port_Queries_InvalidInputEmptyString_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new UrlBuilder(string.Empty,
                ("name", "John"),
                ("key", "864634DA3")));
        }

        [Fact]
        public void Url_Port_Queries_InvalidInputNullParam_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => new UrlBuilder(null,
                ("name", "John"),
                ("key", "864634DA3")));
        }

        [Fact]
        public void Url_Queries_ValidInput_ObjectCreated()
        {
            UrlBuilder url = new UrlBuilder("https://google.com/",
                ("name", "John"),
                ("key", "864634DA3"));

            UriBuilder builder = (UriBuilder)typeof(UrlBuilder)
                .GetField("_uriBuilder", BindingFlags.Instance | BindingFlags.NonPublic) // TODO: can this be improved :D
                .GetValue(url);

            NameValueCollection query = (NameValueCollection)typeof(UrlBuilder)
                .GetField("_queryString", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(url);

            Assert.NotNull(builder);
            Assert.NotNull(query);
        }
    }
}
