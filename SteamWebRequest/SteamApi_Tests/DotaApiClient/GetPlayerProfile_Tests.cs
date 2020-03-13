using SteamApi;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetPlayerProfile method
    /// </summary>
    public class GetPlayerProfile_Tests : ApiTests
    {
        public GetPlayerProfile_Tests(ClientFixture fixture) : base(fixture) { }

        /// <summary>
        /// Test case where player id is valid. Method should return
        /// player's dota 2 profile.
        /// </summary>
        /// <param name="id32">32-bit steam id</param>
        [Theory]
        [InlineData(99765000)]
        [InlineData(147169892)]
        [InlineData(43038812)]
        [InlineData(169919187)]
        [InlineData(150389604)]
        public void PlayerIdDefined_ReturnsPlayerProfile(uint id32)
        {
            var response = DotaApiClient.GetPlayerProfileAsync(id32)
                .Result;
            SleepAfterSendingRequest();

            Assert.True(response.Id32 == id32);
        }
    }
}
