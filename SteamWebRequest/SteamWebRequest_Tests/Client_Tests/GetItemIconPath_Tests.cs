using SteamApiClient.Dota;
using System;
using Xunit;

namespace SWR.Client_Tests
{
    public class GetItemIconPath_Tests : SteamHttpClient_Tests
    {
        [Theory]
        [InlineData("infinite_waves_shoulder")]
        [InlineData("arms_of_the_onyx_crucible_weapon")]
        [InlineData("dark_artistry_hair_model")]
        [InlineData("hollow_ripper")]
        [InlineData("the_rogue_omen_sword")]
        public void ItemDefProvided_ReturnsCDNPath(string itemDef)
        {
            string iconPath = _client.GetItemIconPathAsync(itemDef)
                .Result;
            this.Sleep();

            Assert.NotEmpty(iconPath);
            Assert.StartsWith("icons/econ", iconPath);
        }

        [Theory]
        [InlineData("infinite_waves_shoulder", 0)]
        [InlineData("arms_of_the_onyx_crucible_weapon", 1)]
        [InlineData("hollow_ripper", 3)]
        [InlineData("the_rogue_omen_sword", 1)]
        public void ItemDefAndSizeProvided_ReturnsCDNPath(string itemDef, byte iconType)
        {
            string iconPath = _client.GetItemIconPathAsync(itemDef, iconType: iconType)
                .Result;
            this.Sleep();

            Assert.NotEmpty(iconPath);
            Assert.StartsWith("icons/econ", iconPath);
        }

        [Fact] // TODO: handle AggregateExceptions
        public void ItemDefAndInvalidTypeProvided_ThrowsException()
        {
            Assert.Throws<AggregateException>(() => {
                string iconPath = _client.GetItemIconPathAsync("dark_artistry_hair_model", iconType: 2)
                .Result;
            });
            this.Sleep();
        }
    }
}
