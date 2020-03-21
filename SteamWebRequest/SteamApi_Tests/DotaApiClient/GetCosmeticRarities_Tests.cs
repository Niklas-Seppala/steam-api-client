using SteamApi;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetCosmeticRarities method.
    /// </summary>
    public class GetCosmeticRarities_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetCosmeticRarities_Tests(ClientFixture fixture) : base(fixture) { }


        /// <summary>
        /// Test case for request method being cancelled by CancellationToken.
        /// Method should return failed ApiResponse object that contains thrown
        /// cancellation exception.
        /// </summary>
        [Fact]
        public async Task MethodGotCancelled_RequestFails()
        {
            CancellationTokenSource source = new CancellationTokenSource();

            // Start task to be cancelled
            var task = Task.Run(async () =>
            {
                return await DotaApiClient.GetCosmeticRaritiesAsync(cToken: source.Token);
            });

            // Cancel method
            source.Cancel();

            var response = await task;
            SleepAfterSendingRequest();

            AssertRequestWasCancelled(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for invalid API interface being provided.
        /// Method should return failed ApiResponse object where exception
        /// that caused failure is stored.
        /// </summary>
        [Fact]
        public void InvalidApiInterface_RequestFails()
        {
            var response = DotaApiClient.GetCosmeticRaritiesAsync(apiInterface: "IDota_2_Heroe")
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for invalid API method version being provided.
        /// Method should return failed ApiResponse object where exception
        /// that caused failure is stored.
        /// </summary>
        [Fact]
        public void InvalidMethodVersion_RequestFails()
        {
            var response = DotaApiClient.GetCosmeticRaritiesAsync(version: "v1.2.3")
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for default parameters. Method
        /// should return list of rarities wrapped into
        /// ApiResponse object.
        /// </summary>
        [Fact]
        public void GetCosmeticRaritiesAsync_DefaultParams_ReturnsRarities()
        {
            var response = DotaApiClient.GetCosmeticRaritiesAsync()
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
            Assert.All(response.Contents, rarity =>
            {
                Assert.NotEmpty(rarity.LocalizedName);
                Assert.NotEmpty(rarity.Name);
                Assert.NotEmpty(rarity.Color);
                Assert.NotEqual((uint)0, rarity.Id);
            });
        }


        /// <summary>
        /// Test case where language is provided as parameter.
        /// Method should return list of rarities wrapped into
        /// ApiResponse object.
        /// </summary>
        /// <param name="lang">Language</param>
        /// <param name="locNames">Results in localized language</param>
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
        public void GetCosmeticRaritiesAsync_LanguageProvided_ReturnsLocalizedRarities(string lang,
            string[] locNames)
        {
            var response = DotaApiClient.GetCosmeticRaritiesAsync(lang: lang)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
            Assert.All(response.Contents, rarity => 
            {
                Assert.Contains(locNames, r => r == rarity.LocalizedName);
            });
        }
    }
}
