using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetHeroInfo method
    /// </summary>
    public class GetHeroInfo_Tests : ApiTests
    {
        public GetHeroInfo_Tests(ClientFixture fixture) : base(fixture) { }

        /// <summary>
        /// Tests that method returns filled
        /// dictionary of heroinfos.
        /// </summary>
        /// <param name="heroName">Name of the hero</param>
        [Fact]
        public void DefaultArgs_ReturnsHeroInfo()
        {
            var response = DotaApiClient.GetHeroInfosAsync()
                .Result;
            SleepAfterSendingRequest();

            Assert.NotNull(response);
            Assert.NotEmpty(response);

            Assert.All(response, info =>
            {
                Assert.NotEmpty(info.Value.Name);
                Assert.NotNull(info.Value.Bio);
                Assert.NotEmpty(info.Value.AttackType);
                Assert.NotEmpty(info.Value.Roles);
            });
        }
    }
}
