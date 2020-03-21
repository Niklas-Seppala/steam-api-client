using SteamApi;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetItemIconPath method
    /// </summary>
    public class GetItemIconPath_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetItemIconPath_Tests(ClientFixture fixture) : base(fixture) { }


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
                return await DotaApiClient.GetItemIconPathAsync("infinite_waves_shoulder",
                    cToken: source.Token);
            });

            // Cancel method
            source.Cancel();

            var response = await task;
            SleepAfterSendingRequest();

            AssertRequestWasCancelled(response);
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
            var response = DotaApiClient.GetItemIconPathAsync("infinite_waves_shoulder",
                version: "v1.2.3").Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for itemDef as parameter. Method
        /// should return item icon path wrapped into 
        /// ApiResponse object.
        /// </summary>
        /// <param name="itemDef">item name</param>
        [Theory]
        [InlineData("infinite_waves_shoulder")]
        [InlineData("arms_of_the_onyx_crucible_weapon")]
        [InlineData("dark_artistry_hair_model")]
        [InlineData("hollow_ripper")]
        [InlineData("the_rogue_omen_sword")]
        public void ItemDefProvided_ReturnsCDNPath(string itemDef)
        {
            var response = DotaApiClient.GetItemIconPathAsync(itemDef)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
            Assert.StartsWith("icons/econ", response.Contents);
        }


        /// <summary>
        /// Test case for item id and icon type as parameters.
        /// Method should return item icon path wrapped
        /// into ApiResponse object.
        /// </summary>
        /// <param name="itemDef">Item name</param>
        /// <param name="iconType">Icon type</param>
        [Theory]
        [InlineData("infinite_waves_shoulder", 0)]
        [InlineData("arms_of_the_onyx_crucible_weapon", 1)]
        [InlineData("hollow_ripper", 3)]
        [InlineData("the_rogue_omen_sword", 1)]
        public void ItemDefAndTypeProvided_ReturnsCDNPath(string itemDef,
            uint iconType)
        {
            var response = DotaApiClient.GetItemIconPathAsync(itemDef, iconType: iconType)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
            Assert.StartsWith("icons/econ", response.Contents);
        }


        /// <summary>
        /// Test case for invalid item type as parameter.
        /// Method should return failed ApiResponse object.
        /// </summary>
        [Fact]
        public void ItemDefAndInvalidTypeProvided_ThrowsException()
        {
            var response = DotaApiClient.GetItemIconPathAsync("dark_artistry_hair_model",
                iconType: 2).Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }
    }
}
