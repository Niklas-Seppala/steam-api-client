using System.Threading.Tasks;
using System.Threading;
using Xunit;
using SteamApi;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetItemImage method.
    /// </summary>
    public class GetItemImage_Tests : ApiTests
    {
        /// <summary>
        /// Setup.
        /// </summary>
        public GetItemImage_Tests(ClientFixture fixture) : base(fixture) { }

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
                return await DotaApiClient.GetItemImageAsync("blink",
                    ItemImageShape.Small, cToken: source.Token);
            });

            // Cancel method
            source.Cancel();

            var response = await task;
            SleepAfterSendingRequest();

            AssertRequestWasCancelled(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for different dota 2 items.
        /// Method should return all images.
        /// </summary>
        /// <param name="itemName">Dota 2 item name</param>
        [Theory]
        [InlineData("blink")]
        [InlineData("broadsword")]
        [InlineData("claymore")]
        public void ItemNameDefined_ReturnsImageBytes(string itemName)
        {
            var response = DotaApiClient.GetItemImageAsync(itemName, ItemImageShape.Small)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
        }


        /// <summary>
        /// Test case for all image shapes. Method
        /// should return requested image.
        /// </summary>
        /// <param name="shape">Image shape</param>
        [Theory]
        [InlineData(ItemImageShape.Large, 10959)]
        [InlineData(ItemImageShape.Small, 3273)]
        public void ImageSizeDefined_ReturnsCorrectSize(ItemImageShape shape, int correctSize)
        {
            var response = DotaApiClient.GetItemImageAsync("blink", shape)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
            Assert.Equal(correctSize, response.Contents.Length);
        }
    }


    /// <summary>
    /// Test class for dota 2 api client's GetHeroImage method.
    /// </summary>
    public class GetHeroImage_Tests : ApiTests
    {
        /// <summary>
        /// Setup.
        /// </summary>
        public GetHeroImage_Tests(ClientFixture fixture) : base(fixture) { }

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
                return await DotaApiClient.GetHeroImageAsync("antimage",
                    HeroImageShape.Small, cToken: source.Token);
            });

            // Cancel method
            source.Cancel();

            var response = await task;
            SleepAfterSendingRequest();

            AssertRequestWasCancelled(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for different dota 2 heroes.
        /// Method should return all images.
        /// </summary>
        /// <param name="heroName">Dota 2 hero name</param>
        [Theory]
        [InlineData("antimage")]
        [InlineData("axe")]
        [InlineData("bane")]
        public void HeroNameDefined_ReturnsImagebytes(string heroName)
        {
            var response = DotaApiClient.GetHeroImageAsync(heroName, HeroImageShape.Small)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
        }


        /// <summary>
        /// Test case for all image shapes. Method
        /// should return requested image.
        /// </summary>
        /// <param name="shape">Image shape</param>
        [Theory]
        [InlineData(HeroImageShape.Full, 78978)]
        [InlineData(HeroImageShape.Horizontal, 117074)]
        [InlineData(HeroImageShape.Small, 10818)]
        [InlineData(HeroImageShape.Vertical, 22417)]
        public void ImageSizeDefined_ReturnsCorrectSize(HeroImageShape shape, int correctSize)
        {
            var response = DotaApiClient.GetHeroImageAsync("antimage", shape)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
            Assert.Equal(correctSize, response.Contents.Length);
        }
    }
}
