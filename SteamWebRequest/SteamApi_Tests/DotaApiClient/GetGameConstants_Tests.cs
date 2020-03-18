using Xunit;

namespace Client.Dota
{
    public class GetGameConstants_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetGameConstants_Tests(ClientFixture fixture) : base(fixture) { }


        [Fact]
        public void GetGameItems_DefaultParams_ReturnsGameItems()
        {
            var items = DotaApiClient.GetGameItemsAsync()
                .Result;
            SleepAfterSendingRequest();

            Assert.All(items, item =>
            {
                Assert.NotEmpty(item.LocalizedName);
                Assert.NotEmpty(item.Name);
                Assert.NotEqual((uint)0, item.Id);
            });
        }
    }
}
