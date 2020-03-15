using SteamApi;
using Xunit;

namespace Utility
{
    public class UrlBuilder_Tests
    {
        [Theory]
        [InlineData("etc/bin/home/img.png", "/etc/bin/home/img.png")]
        [InlineData("/home/images", "/home/images")]
        [InlineData("img.jpg", "/img.jpg")]
        public void SetPath_SingleStringInput_SetsPathCorrectly(string path, string pathResult)
        {
            var url = new ApiUrlBuilder();
            url.AppendPath(path);

            Assert.Equal(pathResult, url.Path);
        }

        [Theory]
        [InlineData("/etc/images/trash/img.png", "etc", "images", "trash", "img.png")]
        [InlineData("/img/pic.png", "/img", "/pic.png")]
        [InlineData("/pic.png", "pic.png")]
        [InlineData("/r/", "/r/")]
        public void SetPath_ParamsStrings_SetsPathCorrectly(string resultPath, params string[] path)
        {
            var url = new ApiUrlBuilder();
            url.AppendPath(path);
            
            Assert.Equal(resultPath, url.Path);
        }

        [Fact]
        public void AddQuery_ValidInput_AddsQueryToQuerystring()
        {
            var urlBuilder = new ApiUrlBuilder();
            urlBuilder.AppendQuery("key", "1234");

            Assert.Equal("?key=1234", urlBuilder.Query);
        }

        [Fact]
        public void AddQuery_MultipleValidInputs_AddsQueriesToQuerystring()
        {
            var urlBuilder = new ApiUrlBuilder()
                .AppendQuery("key", "banana")
                .AppendQuery("cat", "black");

            Assert.Equal("?key=banana&cat=black", urlBuilder.Query);
        }
    }
}
