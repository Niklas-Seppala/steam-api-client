using System;
using Xunit;

namespace Client.Steam
{
    public class GetSteamServerInfo_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetSteamServerInfo_Tests(ClientFixture fixture) : base(fixture) { }


        /// <summary>
        /// Test case for normal conditions. Method should return
        /// up to date server info wrapped into ApiResponse object.
        /// </summary>
        [Fact]
        public void ValidRequest_ReturnsSteamServerInfo()
        {
            var response = SteamApiClient.GetSteamServerInfoAsync()
                .Result;
            SleepAfterSendingRequest();

            var responseDate = DateTimeOffset.FromUnixTimeSeconds((long)response.Contents.ServerTime).Date;

            Assert.True(DateTime.Now.Date == responseDate);
        }
    }
}
