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
        /// Test case for default parameters. Method should
        /// return Steam Apilist wrapped into ApiResponse object
        /// </summary>
        [Fact]
        public void DefaultConditions_ReturnsApiList()
        {
            var requestResult = SteamApiClient.GetApiListAsync()
                .Result;
            SleepAfterSendingRequest();

            Assert.True(requestResult.Successful);
            Assert.NotEmpty(requestResult.Contents);
            Assert.All(requestResult.Contents, resp => {

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
