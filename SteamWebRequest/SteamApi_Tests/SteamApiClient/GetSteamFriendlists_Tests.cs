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
        /// Test case for 32-bit steam id. Method should throw Exception
        /// </summary>
        [Fact]
        public void IdIs32Bit_ThrowsHttpRequestException()
        {
            var ex = Assert.Throws<AggregateException>(() =>
            {
                var response = SteamApiClient.GetFriendslistAsync(78123870)
                    .Result;
            }).InnerException as HttpRequestException; // steam api goes full internal server error. nice :)
            SleepAfterSendingRequest();

            Assert.NotNull(ex); // makes sure inner exception was the expected exception.
        }


        /// <summary>
        /// Test case for invalid steam id. Method should throw HttpRequestException
        /// because Steam side has an internal server error.
        /// </summary>
        [Fact]
        public void InvalidId_ThrowsHttpRequestException()
        {
            var ex = Assert.Throws<AggregateException>(() =>
            {
                var response = SteamApiClient.GetFriendslistAsync(0)
                    .Result;
            }).InnerException as HttpRequestException; // steam api goes full internal server error. nice :)
            SleepAfterSendingRequest();

            Assert.NotNull(ex); // makes sure inner exception was the expected exception.
        }


        /// <summary>
        /// Test case for steam profile that is not public. You can't
        /// get private profile's friendslist, sot method should throw
        /// exception informing about the situation.
        /// </summary>
        [Fact]
        public void PrivateProfileId_ThrowsPrivateResponseException()
        {
            var ex = Assert.Throws<AggregateException>(() => {
                var response = SteamApiClient.GetFriendslistAsync(76561198089305067)
                .Result;
            }).InnerException as ApiPrivateContentException;
            SleepAfterSendingRequest();

            Assert.NotNull(ex); // makes sure inner exception was the expected exception.
            Assert.True(401 == ex.StatusCode); // Http status code should be 401 for unauthorized request
        }


        /// <summary>
        /// Test case for valid and visible steam profiles. Method should return
        /// profile's friendslist.
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
            Assert.NotEmpty(response);
            Assert.All(response, r => Assert.True(r.Id64 != 0));
        }
    }
}
