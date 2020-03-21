using SteamApi;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetItemCreators method.
    /// </summary>
    public class GetItemCreators_Tests : ApiTests
    {
        /// <summary>
        /// Setup.
        /// </summary>
        public GetItemCreators_Tests(ClientFixture fixture) : base(fixture) { }


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
                return await DotaApiClient.GetItemCreatorsAsync(6666,
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
            var response = DotaApiClient.GetItemCreatorsAsync(6666,
                version: "v1.2.3").Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case where item id is provided as
        /// parameter. Method should return list of
        /// creator ids wrapped into ApiResponse.
        /// </summary>
        /// <param name="itemDef">Item id</param>
        [Theory]
        [InlineData(6666)]
        [InlineData(5613)]
        [InlineData(5614)]
        [InlineData(5615)]
        public void ItemIdWithCreatorsProvided_ReturnsItemCreators(uint itemDef)
        {
            var response = DotaApiClient.GetItemCreatorsAsync(itemDef).Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
        }


        /// <summary>
        /// Test case where item id is provided, but
        /// item has no creators. Method should return
        /// failed ApiResponse object.
        /// </summary>
        /// <param name="itemDef">Item id</param>
        [Theory]
        [InlineData(418)]
        [InlineData(419)]
        [InlineData(420)]
        [InlineData(421)]
        public void ItemIdWithNoCreatorsProvided_ReturnsEmptyCreators(uint itemDef)
        {
            var response = DotaApiClient.GetItemCreatorsAsync(itemDef).Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }
    }
}
