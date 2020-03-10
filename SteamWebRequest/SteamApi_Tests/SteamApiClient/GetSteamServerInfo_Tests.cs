using System;
using Xunit;

namespace Client
{
    public class GetSteamServerInfo_Tests : SteamApiClientTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetSteamServerInfo_Tests(ClientFixture fixture) : base(fixture) { }


        /// <summary>
        /// Test case for normal conditions. Method should return
        /// up to date server info.
        /// </summary>
        [Fact]
        public void ValidRequest_ReturnsSteamServerInfo()
        {
            var response = SteamApiClient.GetSteamServerInfoAsync()
                .Result;
            SleepAfterSendingRequest();

            var responseDate = DateTimeOffset.FromUnixTimeSeconds((long)response.ServerTime).Date;

            Assert.True(DateTime.Now.Date == responseDate);
        }
    }
}
