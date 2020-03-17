using SteamApi;
using Xunit;
using System.Collections.Generic;

namespace Client.Steam
{
    /// <summary>
    /// Unit test class for GetSteamAccount-methods.
    /// </summary>
    public class GetSteamUsers_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetSteamUsers_Tests(ClientFixture fixture) : base(fixture) { }


        /// <summary>
        /// Tests Steam api client's method that requests for multiple steam profiles
        /// using faulty 64-bit steam ids.
        /// </summary>
        [Fact]
        public void MultipleFaultyIds_ReturnsEmptyAccountCollection()
        {
            ulong[] ids = new ulong[] { 0, 0, 0 };

            var response = SteamApiClient.GetSteamAccountsAsync(ids)
                .Result;
            SleepAfterSendingRequest();

            Assert.Empty(response.Contents);
        }


        /// <summary>
        /// Tests Steam api client's method that requests for single steam profile
        /// using faulty 64-bit steam id.
        /// </summary>
        [Fact]
        public void SingleFaultyId_ThrowsEmptyApiResponseException()
        {
            var response = SteamApiClient.GetSteamAccountAsync(0)
                .Result;
            SleepAfterSendingRequest();

            Assert.False(response.Successful);
            Assert.NotNull(response.ThrownException);
            Assert.True(response.ThrownException is ApiEmptyResultException);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Tests Steam api client's method that requests for single steam profile
        /// using 64-bit steam id
        /// </summary>
        [Fact]
        public void SingleId_ReturnsRequestedProfile()
        {
            var response = SteamApiClient.GetSteamAccountAsync(76561197960321706)
                .Result;
            SleepAfterSendingRequest();

            Assert.True(response.Contents.Id64 == 76561197960321706);
            Assert.NotEmpty(response.Contents.PersonaName);
            Assert.NotEmpty(response.Contents.AvatarMediumURL);
            Assert.NotEmpty(response.Contents.AvatarFullURL);
            Assert.NotEmpty(response.Contents.AvatarSmallURL);
        }


        /// <summary>
        /// Tests SteamApiClient's method that requests for multiple steam profiles
        /// using an array of 64-bit steam ids
        /// </summary>
        /// <param name="id64s">Array of steam ids</param>
        /// <param name="count">Size of the id collection</param>
        [Theory]
        [InlineData(new ulong[] { }, 0)]
        [InlineData(new ulong[] { 76561198096280303 }, 1)]
        [InlineData(new ulong[] { 76561197960321706, 76561198096280303 }, 2)]
        [InlineData(new ulong[] { 76561198049597855, 76561197967026980, 76561198049624886 }, 3)]
        [InlineData(new ulong[] { 76561198089305067, 76561198000860315, 76561198016178791, 76561198067725818 }, 4)]
        public void MultipleIds_ReturnsAllRequestedProfiles(IEnumerable<ulong> id64s, int count)
        {
            var response = SteamApiClient.GetSteamAccountsAsync(id64s)
                .Result;

            SleepAfterSendingRequest();

            Assert.True(response.Contents.Count == count);
            Assert.All(response.Contents, p =>
            {
                Assert.Contains(id64s, id => id == p.Id64);
                Assert.NotEmpty(p.AvatarMediumURL);
                Assert.NotEmpty(p.AvatarFullURL);
                Assert.NotEmpty(p.AvatarSmallURL);
            });
        }
    }
}