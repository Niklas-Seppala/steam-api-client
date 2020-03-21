using SteamApi;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetPlayerProfile method
    /// </summary>
    public class GetPlayerProfile_Tests : ApiTests
    {
        public GetPlayerProfile_Tests(ClientFixture fixture) : base(fixture) { }

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
                return await DotaApiClient.GetTopLiveGamesAsync(cToken: source.Token);
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
            var response = DotaApiClient.GetHeroesAsync(apiInterface: "IDota_2_Players")
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
            var response = DotaApiClient.GetHeroesAsync(version: "v1.99")
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case where player id is valid. Method should return
        /// player's dota 2 profile wrapped into ApiResponse object.
        /// </summary>
        /// <param name="id32">32-bit steam id</param>
        [Theory]
        [InlineData(99765000)]
        [InlineData(147169892)]
        [InlineData(43038812)]
        [InlineData(169919187)]
        [InlineData(150389604)]
        public void PlayerIdDefined_ReturnsPlayerProfile(uint id32)
        {
            var response = DotaApiClient.GetPlayerProfileAsync(id32)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.Equal(response.Contents.Id32, id32);
        }
    }
}
