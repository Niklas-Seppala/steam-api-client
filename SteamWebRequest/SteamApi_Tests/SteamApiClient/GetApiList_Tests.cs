using Xunit;

namespace Client.Steam
{
    public class GetApiList_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetApiList_Tests(ClientFixture fixture) : base(fixture) { }


        /// <summary>
        /// Test case for default parameters
        /// </summary>
        [Fact]
        public void DefaultConditions_ReturnsApiList()
        {
            var response = SteamApiClient.GetApiListAsync()
                .Result;
            SleepAfterSendingRequest();

            Assert.NotEmpty(response);
            Assert.All(response, resp => {

                Assert.NotNull(resp.Methods);
                Assert.NotEmpty(resp.Methods);
                Assert.NotEmpty(resp.Name);
                Assert.All(resp.Methods, method => {
                    Assert.NotEmpty(method.Name);
                    Assert.True(method.Version != 0);
                    Assert.NotEmpty(method.HttpMethod);
                });
            });
        }
    }
}
