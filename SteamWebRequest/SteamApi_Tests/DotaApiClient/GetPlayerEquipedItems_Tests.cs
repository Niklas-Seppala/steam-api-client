using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetPlayerEquipedItems method
    /// </summary>
    public class GetPlayerEquipedItems_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetPlayerEquipedItems_Tests(ClientFixture fixture) : base(fixture) { }


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
                return await DotaApiClient.GetEquipedPlayerItemsAsync(76561198107435620,
                    1, cToken: source.Token);
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
            var response = DotaApiClient.GetEquipedPlayerItemsAsync(76561198107435620,
                1, apiInterface: "IDota_2_Cosmetics").Result;
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
            var response = DotaApiClient.GetEquipedPlayerItemsAsync(76561198107435620,
                1, version: "v8.3").Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for valid and visible player
        /// ids and valid hero id as parameters. Method should return
        /// List of equiped items for specified hero
        /// wrapped into ApiResponse object.
        /// </summary>
        /// <param name="id64">Player 32-bit id.</param>
        /// <param name="heroId">Dota 2 Hero id.</param>
        [Theory]
        [InlineData(76561198107435620, 666)]
        [InlineData(76561198038389598, 198)]
        public void ValidPlayerIdInvalidHeroId_ReturnsPlayersEquipedItems(ulong id64,
            uint heroId)
        {
            var response = DotaApiClient.GetEquipedPlayerItemsAsync(id64, heroId).Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for valid and visible player
        /// ids and valid hero id as parameters. Method should return
        /// List of equiped items for specified hero
        /// wrapped into ApiResponse object.
        /// </summary>
        /// <param name="id64">Player 64-bit id.</param>
        /// <param name="heroId">Dota 2 Hero id.</param>
        /// 
        [Theory]
        [InlineData(76561198107435620, 1)]
        [InlineData(76561198038389598, 20)]
        [InlineData(76561198038389598, 8)]
        [InlineData(76561198038389598, 88)]
        public void ValidPlayerIdValidHeroId_ReturnsPlayersEquipedItems(ulong id64,
            uint heroId)
        {
            var response = DotaApiClient.GetEquipedPlayerItemsAsync(id64, heroId).Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
        }


        /// <summary>
        /// Test case where player profile is not
        /// visible to API key holder. Method should
        /// return failed ApiResponse object.
        /// </summary>
        [Fact]
        public void GetEquipedPlayerItems_PrivateAccount_Returns()
        {
            var response = DotaApiClient.GetEquipedPlayerItemsAsync(76561198059119066, 1).Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }
    }
}
