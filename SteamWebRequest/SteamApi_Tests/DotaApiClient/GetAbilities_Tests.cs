using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota api client's GetAbilities method
    /// </summary>
    public class GetAbilities_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetAbilities_Tests(ClientFixture fixture) : base(fixture) { }


        [Fact]
        public void DefaultParams_ReturnsAbilitites()
        {
            var response = DotaApiClient.GetAbilitiesAsync()
                .Result;
            SleepAfterSendingRequest();

            Assert.NotNull(response);
            Assert.NotEmpty(response);

            Assert.All(response, info =>
            {
                Assert.NotEmpty(info.Value.Affects);
                Assert.NotNull(info.Value.Attribute);
                Assert.NotEmpty(info.Value.LocalizedName);
            });
        }
    }
}
