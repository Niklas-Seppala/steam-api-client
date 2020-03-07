using System;
using System.Collections.Generic;
using Xunit;
using SteamApi;
using SteamApi.Models.Steam;
using System.Linq;

namespace Client
{
    public class GetSteamAccountBans_Tests : SteamApiClientTests
    {
        public GetSteamAccountBans_Tests(ClientFixture fixture) : base(fixture) {}

        /// <summary>
        /// Test case for single valid id. Method should return
        /// correct ban history for requested account
        /// </summary>
        [Fact]
        public void SingleValidId_ReturnsAccountBans()
        {
            var bans = SteamApiClient.GetSteamAccountBansAsync(76561197960321706)
                .Result;
            SleepAfterSendingRequest();

            Assert.True(76561197960321706 == bans.Id64);
        }


        /// <summary>
        /// Test case for single invalid id. Method should
        /// throw EmptyApiResponseException.
        /// </summary>
        [Fact]
        public void SingleInvalidId_ThrowsEmptyApiResponseException()
        {
            var ex = Assert.Throws<AggregateException>(() =>
            {
                var accountBans = SteamApiClient.GetSteamAccountBansAsync(0)
               .Result;
            }).InnerException as EmptyApiResponseException<AccountBans>;
            SleepAfterSendingRequest();

            Assert.NotNull(ex);
            Assert.True(ex.ResponseModelType == typeof(AccountBans));
        }


        /// <summary>
        /// Test case for multiple valid Ids. Method should return
        /// all the account bans.
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
            var accountBans = SteamApiClient.GetSteamAccountsBansAsync(id64s)
                .Result;
            SleepAfterSendingRequest();

            Assert.Empty(accountBans);
        }


        /// <summary>
        /// Test case for multiple valid Ids. Method should return
        /// all the account bans.
        /// </summary>
        /// <param name="id64s">collection of 64-bit steam ids</param>
        [Theory]
        [InlineData(new ulong[] {  })]
        [InlineData(new ulong[] { 76561197960321706 })]
        [InlineData(new ulong[] { 76561197960321706, 76561198096280303  })]
        [InlineData(new ulong[] { 76561197960321706, 76561198096280303, 76561198038389598 })]
        [InlineData(new ulong[] { 76561197960321706, 76561198096280303, 76561198038389598, 76561198107435620 })]
        public void ValidIds_ReturnsAccountBans(IEnumerable<ulong> id64s)
        {
            var accountBans = SteamApiClient.GetSteamAccountsBansAsync(id64s)
                .Result;
            SleepAfterSendingRequest();

            Assert.Equal(accountBans.Count, id64s.Count());
            Assert.All(accountBans, ban =>
            {
                Assert.Contains(id64s, id => id == ban.Id64);
            });
        }
    }
}
