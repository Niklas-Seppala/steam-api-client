using System;
using Xunit;
using SteamApi;

namespace Utility
{
    public class UrlBuilder_Tests
    {
        [Theory]
        [InlineData("liibalala")]
        [InlineData("this is not vali url")]
        [InlineData("")]
        [InlineData("121254325dddfafa66sddd/gege.png")]
        public void Contruction_FromInvalidString_CreatesEmptyObject(string url)
        {
            UrlBuilder ub = new UrlBuilder(url);
            Assert.Equal("", ub.Url);
        }

        [Theory]
        [InlineData("https://example.com/media/upload/filename.jpg", "https",
            "example.com", "/media/upload/filename.jpg", "", "")]
        [InlineData("https://docs.microsoft.com/en-us/dotnet/api/system.double?view=netframework-4.8",
            "https", "docs.microsoft.com", "/en-us/dotnet/api/system.double", "view=netframework-4.8", "")]
        [InlineData("https://stackoverflow.com/questions/16309286/funct-vs-funct-invoke",
            "https", "stackoverflow.com", "/questions/16309286/funct-vs-funct-invoke", "", "")]
        [InlineData("https://github.com/NikkeArp?tab=repositories", "https", "github.com",
            "/NikkeArp", "tab=repositories", "")]
        [InlineData("http://www.reddit.com/r/softwaregore/#jebou", "http", "www.reddit.com",
            "/r/softwaregore/", "", "jebou")]
        public void Construction_FromUrlString_CreatesValidObject(string url,
            string schema, string host, string path, string query, string fragment)
        {
            UrlBuilder ub = new UrlBuilder(url);

            Assert.Equal(schema, ub.Components.Schema);
            Assert.Equal(host, ub.Components.Host);
            Assert.Equal(path, ub.Components.Path);
            Assert.Equal(query, ub.Components.Query);
            Assert.Equal(fragment, ub.Components.Fragment);
        }

        [Theory]
        [InlineData("example.com/media/upload/filename.jpg",
            "", "example.com", "/media/upload/filename.jpg", "", "")]
        [InlineData("docs.microsoft.com/en-us/dotnet/api/system.double?view=netframework-4.8",
            "", "docs.microsoft.com", "/en-us/dotnet/api/system.double", "view=netframework-4.8", "")]
        [InlineData("stackoverflow.com/questions/16309286/funct-vs-funct-invoke",
            "", "stackoverflow.com", "/questions/16309286/funct-vs-funct-invoke", "", "")]
        [InlineData("github.com/NikkeArp?tab=repositories#repo", "", "github.com",
            "/NikkeArp", "tab=repositories", "repo")]
        [InlineData("www.reddit.com/r/softwaregore/#asd", "", "www.reddit.com",
            "/r/softwaregore/", "", "asd")]
        public void Constructor_FromUrlStringWithoutSchema_CreatesValidObject(string url,
            string schema, string host, string path, string query, string fragment)
        {
            UrlBuilder ub = new UrlBuilder(url);

            Assert.Equal(schema, ub.Components.Schema);
            Assert.Equal(host, ub.Components.Host);
            Assert.Equal(path, ub.Components.Path);
            Assert.Equal(query, ub.Components.Query);
            Assert.Equal(fragment, ub.Components.Fragment);
        }

        [Theory]
        [InlineData("localhost:8080", "localhost", "", "", 8080)]
        [InlineData("127.0.0.1:80", "127.0.0.1", "", "", 80)]
        [InlineData("webserver:443/dir1/dir2", "webserver", "/dir1/dir2", "", 443)]
        
        public void Construction_FromStringWithValidPortUrl_CreatesValidObject(string url,
            string host, string path, string query, int port)
        {
            UrlBuilder ub = new UrlBuilder(url);

            Assert.Equal(host, ub.Components.Host);
            Assert.Equal(port, ub.Components.Port);
            Assert.Equal(path, ub.Components.Path);
            Assert.Equal(query, ub.Components.Query);
        }

        [Fact]
        public void UrlFromString_ValidStringUrl_UpdatesUrl()
        {
            UrlBuilder urlBuilder = new UrlBuilder();
            urlBuilder.UrlFromString("localhost:8080");

            Assert.Equal("", urlBuilder.Components.Schema);
            Assert.Equal("", urlBuilder.Components.Path);
            Assert.Equal("", urlBuilder.Components.Query);
            Assert.Equal("", urlBuilder.Components.Fragment);
            Assert.Equal("localhost", urlBuilder.Components.Host);
            Assert.Equal(8080, urlBuilder.Components.Port);
        }

        [Fact]
        public void UrlFromString_ContinousUse_ClearsPreviousUrl()
        {
            UrlBuilder urlBuilder = new UrlBuilder("https://en.wikipedia.org/wiki/URL");
            urlBuilder.UrlFromString("localhost:8080");

            Assert.Equal("localhost", urlBuilder.Components.Host);
            Assert.Equal(8080, urlBuilder.Components.Port);
            Assert.Equal("", urlBuilder.Components.Path);
            Assert.Equal("", urlBuilder.Components.Schema);
        }

        [Theory]
        [InlineData("etc/bin/home/img.png", "/etc/bin/home/img.png")]
        [InlineData("/home/images", "/home/images")]
        [InlineData("img.jpg", "/img.jpg")]
        public void SetPath_SingleStringInput_SetsPathCorrectly(string path, string pathResult)
        {
            UrlBuilder urlBuilder = new UrlBuilder();
            urlBuilder.SetPath(path);

            Assert.Equal(pathResult, urlBuilder.Components.Path);
        }

        [Theory]
        [InlineData("/etc/images/trash/img.png", "etc", "images", "trash", "img.png")]
        [InlineData("/img/pic.png", "/img", "/pic.png")]
        [InlineData("/pic.png", "pic.png")]
        [InlineData("/r/", "/r/")]
        public void SetPath_ParamsStrings_SetsPathCorrectly(string resultPath, params string[] path)
        {
            UrlBuilder urlBuilder = new UrlBuilder();

            urlBuilder.SetPath(path);
            Assert.Equal(resultPath, urlBuilder.Components.Path);
        }

        [Fact]
        public void AddQuery_ValidInput_AddsQueryToQuerystring()
        {
            UrlBuilder urlBuilder = new UrlBuilder();
            urlBuilder.AddQuery("key", "1234");

            Assert.Equal("key=1234", urlBuilder.Components.Query);
        }

        [Fact]
        public void AddQuery_MultipleValidInputs_AddsQueriesToQuerystring()
        {
            UrlBuilder urlBuilder = new UrlBuilder()
                .AddQuery("key", "banana")
                .AddQuery("cat", "black");

            Assert.Equal("key=banana&cat=black", urlBuilder.Components.Query);
        }

        [Fact]
        public void RemoveQuery_ValidInput_RemovesQueryFromUrl()
        {
            UrlBuilder urlBuilder = new UrlBuilder()
                .AddQuery("key", "banana")
                .AddQuery("cat", "black");
            urlBuilder.RemoveQuery("cat");

            Assert.Equal("key=banana", urlBuilder.Components.Query);
        }

        [Fact]
        public void ClearQueries__ClearsWholeQueryString()
        {
            UrlBuilder urlBuilder = new UrlBuilder()
                .AddQuery("key", "banana")
                .AddQuery("cat", "black");
            urlBuilder.ClearQueries();

            Assert.Equal("", urlBuilder.Components.Query);
        }

        [Fact]
        public void RemoveQuery_InvalidInput_NothingHappens()
        {
            UrlBuilder urlBuilder = new UrlBuilder()
                .AddQuery("key", "banana")
                .AddQuery("cat", "black");
            urlBuilder.RemoveQuery("not_a_key");

            Assert.Equal("key=banana&cat=black", urlBuilder.Components.Query);
        }

        [Fact]
        public void SetHost_ValidInput_InsertsHostToUrl()
        {
            UrlBuilder urlBuilder = new UrlBuilder("example.com/media/upload")
                .SetHost("google.com");

            Assert.Equal("google.com", urlBuilder.Components.Host);
        }

        [Fact]
        public void SetFragment_ValidInput_AppendsFragmentToUrl()
        {
            UrlBuilder urlBuilder = new UrlBuilder("example.com/media/upload")
                .SetFragment("link");

            Assert.Equal("link", urlBuilder.Components.Fragment);
        }

        [Fact]
        public void SetPort_ValidInput_AppendsFragmentToUrl()
        {
            UrlBuilder urlBuilder = new UrlBuilder("example.com/media/upload")
                .SetPort(8080);

            Assert.Equal(8080, urlBuilder.Components.Port);
            Assert.Equal("example.com:8080/media/upload", urlBuilder.Url);
        }

        [Fact]
        public void SetPath_ValidInput_AppendsFragmentToUrl()
        {
            UrlBuilder urlBuilder = new UrlBuilder("example.com/media/upload")
                .SetPath("bin", "ban");

            Assert.Equal("/bin/ban", urlBuilder.Components.Path);
            Assert.Equal("example.com/bin/ban", urlBuilder.Url);
        }

        [Fact]
        public void SetSchema_ValidInput_InsertsSchemaToUrl()
        {
            UrlBuilder urlBuilder = new UrlBuilder("example.com/media/upload")
                .SetSchema("https");

            Assert.Equal("https", urlBuilder.Components.Schema);
            Assert.Equal("https://example.com/media/upload", urlBuilder.Url);
        }

        [Theory]
        [InlineData("https://google.com/‰‰kkˆset?Â‰=</>>",
            "https://google.com/%C3%A4%C3%A4kk%C3%B6set?%C3%A5%C3%A4=%3C%2F%3E%3E")]
        [InlineData("http://‰itiensivu.fi", "http://‰itiensivu.fi")]
        [InlineData("http://‰itiensivu.fi?pasta=hyv‰‰",
            "http://‰itiensivu.fi?pasta=hyv%C3%A4%C3%A4")]
        [InlineData("http://‰itiensivu.fi?pasta=hyv‰‰#ÂÂ",
            "http://‰itiensivu.fi?pasta=hyv%C3%A4%C3%A4#%C3%A5%C3%A5")]
        public void EncodeUrl_UrlWithUnicode_EncodesOutputUrl(string url, string encodedUrl)
        {
            UrlBuilder urlBuilder = new UrlBuilder(url);
            Assert.Equal(encodedUrl, urlBuilder.Url, true);
        }

        [Fact]
        public void PopUrl_ReturnsUrlAndClearsAllFields()
        {
            UrlBuilder urlBuilder =
                new UrlBuilder("https://example.com:666/media/upload/filename.jpg?key=123#nice_pic");

            string url = urlBuilder.PopUrl();
            Assert.Equal("https://example.com:666/media/upload/filename.jpg?key=123#nice_pic", url);
            Assert.Equal("", urlBuilder.Components.Fragment);
            Assert.Equal("", urlBuilder.Components.Host);
            Assert.Equal("", urlBuilder.Components.Path);
            Assert.Equal(-1, urlBuilder.Components.Port);
            Assert.Equal("", urlBuilder.Components.Query);
        }
    }
}
