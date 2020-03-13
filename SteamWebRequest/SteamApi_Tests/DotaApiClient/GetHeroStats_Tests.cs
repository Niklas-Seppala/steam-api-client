using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetHeroStats method
    /// </summary>
    public class GetHeroStats_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetHeroStats_Tests(ClientFixture fixture) : base(fixture) { }


        /// <summary>
        /// Tests that method returns filled
        /// dictionary of herostats.
        /// </summary>
        /// <param name="heroName">name of the hero</param>
        [Fact]
        public void DefaultParams_ReturnsHeroStats()
        {
            var response = DotaApiClient.GetHeroStatsAsync()
                .Result;
            SleepAfterSendingRequest();

            Assert.NotNull(response);
            Assert.NotEmpty(response);

            Assert.All(response, stats =>
            {
                Assert.NotEmpty(stats.Value.Name);
                Assert.NotNull(stats.Value.Attributes);
                Assert.NotEmpty(stats.Value.LocalizedName);
                Assert.NotEmpty(stats.Value.PrimaryAttribute);
            });
        }

    }
}
