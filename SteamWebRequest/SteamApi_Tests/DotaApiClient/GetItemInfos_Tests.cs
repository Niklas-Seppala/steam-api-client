using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetItemInfos method
    /// </summary>
    public class GetItemInfos_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetItemInfos_Tests(ClientFixture fixture) : base(fixture) { }

        /// <summary>
        /// Tests that method returns filled
        /// dictionary of herostats.
        /// </summary>
        /// <param name="itemName">name of the hero</param>
        [Fact]
        public void DefaultParams_ReturnsItemInfos()
        {
            var response = DotaApiClient.GetItemInfosAsync()
                .Result;
            SleepAfterSendingRequest();

            Assert.NotNull(response);
            Assert.NotEmpty(response);

            Assert.All(response, item => {
                Assert.True(item.Value.Id != 0);
            });
        }

    }
}
