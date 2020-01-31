using Xunit;
using SteamApiClient.Dota;

namespace SWR.Client_Tests
{
    public class GetStoreMetaData_Tests : SteamHttpClient_Tests
    {
        [Fact]
        public void DefaultParams_ReturnsStoreMetaData()
        {
            var metaData = _client.GetStoreMetadataAsync()
                .Result;
            this.Sleep();

            Assert.NotNull(metaData.DropDownData);
            Assert.NotNull(metaData.Filters);
            Assert.NotNull(metaData.HomePageData);
            Assert.NotNull(metaData.PlayerClassData);
            Assert.NotNull(metaData.Sorting);
            Assert.NotNull(metaData.Tabs);
        }
    }

}
