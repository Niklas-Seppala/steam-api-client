using SteamApiClient.Dota;
using Xunit;

namespace SWR.Client_Tests
{
    public class GetRarities_Tests : SteamHttpClient_Tests
    {
        [Fact]
        public void DefaultParams_ReturnsRarities()
        {
            var rarities = _client.GetDotaCosmeticRaritiesAsync()
                .Result;
            this.Sleep();

            Assert.NotEmpty(rarities);
            Assert.All(rarities, rarity => {
                Assert.NotEmpty(rarity.LocalizedName);
                Assert.NotEmpty(rarity.Name);
                Assert.NotEmpty(rarity.Color);
                Assert.NotEqual(0, rarity.Id);
            });
        }

        [Theory]
        [InlineData("fi", 
            new string[] {
                "Yleinen", "Tavaton", "Harvinainen", "Myyttinen",
                "Tarunhohtoinen", "Muinainen", "Kuolematon", "Salatieteellinen" 
            })]
         [InlineData("de", 
            new string[] {
                "Gewöhnlich", "Ungewöhnlich", "Rare", "Mythical",
                "Legendär", "Ancient", "Immortal", "Arkana"
            })]
        [InlineData("fr",
            new string[] {
                "Commun", "Peu commun", "Rare", "Mythique",
                "Légendaire", "Ancien", "Immortel", "Arcane"
            })]
        public void LanguageProvided_ReturnsLocalizedRarities(string lang, string[] locNames)
        {
            var rarities = _client.GetDotaCosmeticRaritiesAsync(lang: lang)
                .Result;
            this.Sleep();

            foreach (string item in locNames)
            {
                Assert.Contains(rarities, rarity => rarity.LocalizedName == item);
            }
        }
    }
}
