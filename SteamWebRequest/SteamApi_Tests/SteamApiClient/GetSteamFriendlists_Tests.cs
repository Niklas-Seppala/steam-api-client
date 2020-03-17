using System;
using Xunit;
using SteamApi;
using System.Net.Http;

namespace Client.Steam
{
    public class GetSteamFriendlists_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetSteamFriendlists_Tests(ClientFixture fixture) : base(fixture){ }


        /// <summary>
        /// Test case for 32-bit steam id. Method should return
        /// null content wrapped into failed ApiResponse object.
        /// </summary>
        [Fact]
        public void IdIs32Bit_ThrowsHttpRequestException()
        {
            var response = SteamApiClient.GetFriendslistAsync(78123870)
                .Result;

            Assert.False(response.Successful);
            Assert.Null(response.Contents);
            Assert.NotNull(response.ThrownException);
            Assert.True(response.ThrownException is HttpRequestException);
        }


        /// <summary>
        /// Test case for invalid steam id. Because Steam
        /// side has an internal server error, returns null
        /// content wrapped into failied ApiResponse object.
        /// </summary>
        [Fact]
        public void InvalidId_ThrowsHttpRequestException()
        {
            var response = SteamApiClient.GetFriendslistAsync(0)
                .Result;

            Assert.False(response.Successful);
            Assert.NotNull(response.ThrownException);
            Assert.Null(response.Contents);
            Assert.True(response.ThrownException is HttpRequestException);
        }


        /// <summary>
        /// Test case for steam profile that is not public. You can't
        /// get private profile's friendslist, so method returns
        /// null content wrapped into failed ApiResponse object.
        /// </summary>
        [Fact]
        public void PrivateProfileId_ThrowsPrivateResponseException()
        {
            var response = SteamApiClient.GetFriendslistAsync(76561198089305067)
                .Result;
            SleepAfterSendingRequest();

            Assert.False(response.Successful);
            Assert.NotNull(response.ThrownException);
            Assert.Null(response.Contents);
            Assert.True(response.ThrownException is ApiPrivateContentException);
        }


        /// <summary>
        /// Test case for valid and visible steam profiles. Method should return
        /// profile's friendslist wrapped into ApiResponse object.
        /// </summary>
        /// <param name="id64"></param>
        [Theory]
        [InlineData(76561198096280303)]
        [InlineData(76561197967026980)]
        [InlineData(76561198016178791)]
        [InlineData(76561198067725818)]
        public void ValidId_ReturnsUsersFriendslist(ulong id64)
        {
            var response = SteamApiClient.GetFriendslistAsync(id64)
                .Result;
            SleepAfterSendingRequest();

            Assert.NotNull(response);
            Assert.NotEmpty(response.Contents);
            Assert.All(response.Contents, r => Assert.True(r.Id64 != 0));
        }
    }
}
