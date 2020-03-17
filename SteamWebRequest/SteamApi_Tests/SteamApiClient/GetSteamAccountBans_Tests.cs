using System.Collections.Generic;
using Xunit;
using SteamApi;
using System.Linq;

namespace Client.Steam
{
    public class GetSteamAccountBans_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetSteamAccountBans_Tests(ClientFixture fixture) : base(fixture) {}


        /// <summary>
        /// Test case for single valid id. Method should return
        /// correct ban history for requested account wrapped into
        /// ApiResponse object.
        /// </summary>
        [Fact]
        public void SingleValidId_ReturnsAccountBans()
        {
            var response = SteamApiClient.GetSteamAccountBansAsync(76561197960321706)
                .Result;
            SleepAfterSendingRequest();

            Assert.True(76561197960321706 == response.Contents.Id64);
        }


        /// <summary>
        /// Test case for single invalid id. Method should
        /// return null content wrapped into failed Apiresponse
        /// object.
        /// </summary>
        [Fact]
        public void SingleInvalidId_ThrowsEmptyApiResponseException()
        {
            var response = SteamApiClient.GetSteamAccountBansAsync(0)
               .Result;

            Assert.False(response.Successful);
            Assert.Null(response.Contents);
            Assert.NotNull(response.ThrownException);
            Assert.True(response.ThrownException is ApiEmptyResultException);
        }


        /// <summary>
        /// Test case for multiple valid Ids. Method should return
        /// all the account bans wrapped into ApiResponse object
        /// </summary>
        /// <param name="id64s">collection of 64-bit steam ids</param>
        [Theory]
        [InlineData(new ulong[] { })]
        [InlineData(new ulong[] { 0 })]
        [InlineData(new ulong[] { 0, 0 })]
        [InlineData(new ulong[] { 0, 0, 0 })]
        [InlineData(new ulong[] { 0, 0, 0, 0 })]
        public void InvalidIds_ReturnsEmptyBanCollection(IEnumerable<ulong> id64s)
        {
            var response = SteamApiClient.GetSteamAccountsBansAsync(id64s)
                .Result;
            SleepAfterSendingRequest();

            Assert.False(response.Successful);
            Assert.Null(response.Contents);
            Assert.NotNull(response.ThrownException);
        }


        /// <summary>
        /// Test case for multiple valid Ids. Method should return
        /// all the account bans wrapped into ApiResponse model
        /// </summary>
        /// <param name="id64s">collection of 64-bit steam ids</param>
        [Theory]
        [InlineData(new ulong[] { 76561197960321706 })]
        [InlineData(new ulong[] { 76561197960321706, 76561198096280303  })]
        [InlineData(new ulong[] { 76561197960321706, 76561198096280303, 76561198038389598 })]
        [InlineData(new ulong[] { 76561197960321706, 76561198096280303, 76561198038389598, 76561198107435620 })]
        public void ValidIds_ReturnsAccountBans(IEnumerable<ulong> id64s)
        {
            var response = SteamApiClient.GetSteamAccountsBansAsync(id64s)
                .Result;
            SleepAfterSendingRequest();

            Assert.True(response.Successful);
            Assert.Equal(response.Contents.Count, id64s.Count());
            Assert.All(response.Contents, ban =>
            {
                Assert.Contains(id64s, id => id == ban.Id64);
            });
        }
    }
}
