using SteamApi;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetPlayerItems method
    /// </summary>
    public class GetPlayerItems_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetPlayerItems_Tests(ClientFixture fixture) : base(fixture) { }


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
                return await DotaApiClient.GetPlayerItemsAsync(76561198107435620,
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
        /// Test case for invalid API interface being provided.
        /// Method should return failed ApiResponse object where exception
        /// that caused failure is stored.
        /// </summary>
        [Fact]
        public void InvalidApiInterface_RequestFails()
        {
            var response = DotaApiClient.GetPlayerItemsAsync(76561198107435620,
                apiInterface: "IDota_2_Cosmetics").Result;
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
            var response = DotaApiClient.GetPlayerItemsAsync(76561198107435620,
                version: "v8.3").Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        [Theory]
        [InlineData(76561198107435620)]
        [InlineData(76561198038389598)]
        public void ValidPlayerId_ReturnsPlayersItems(ulong id)
        {
            var response = DotaApiClient.GetPlayerItemsAsync(id)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents.Items);

            Assert.All(response.Contents.Items, item =>
            {
                Assert.NotEqual((ulong)0, item.Id);
            }); 
        }


        /// <summary>
        /// Test case where requested player is not
        /// visible to API key holder. Method should
        /// return failed ApiResponse object.
        /// </summary>
        [Fact]
        public void PrivateInventory_ReturnsWithStatus15()
        {
            var response = DotaApiClient.GetPlayerItemsAsync(76561198059119066)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            var thrown = response.ThrownException as ApiException;
            Assert.Equal((uint)15, thrown.ApiStatusCode);
        }
    }
}
