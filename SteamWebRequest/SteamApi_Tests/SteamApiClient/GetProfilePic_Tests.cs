using System;
using Xunit;
using SteamApi;
using System.Net.Http;

namespace Client
{
    public class GetProfilePic_Tests : SteamApiClientTests
    {
        public GetProfilePic_Tests(ClientFixture fixture) : base(fixture) {}

        [Fact]
        public void InvalidFileName_ThrowsHttpRequestException()
        {
            var profile = SteamApiClient.GetSteamAccountAsync(76561197960321706)
               .Result;

            // Mess up the profile avatar URL
            if (profile.AvatarFullURL.Contains("jpg"))
                profile.AvatarFullURL = profile.AvatarFullURL.Replace("jpg", "png");
            else
                profile.AvatarFullURL = profile.AvatarFullURL.Replace("png", "jpg");


            var ex = Assert.Throws<AggregateException>(() =>
            {
                byte[] smallBytes = SteamApiClient.GetProfilePicBytesAsync(profile.AvatarFullURL)
                   .Result;
            }).InnerException as ApiResourceNotFoundException;
            Assert.NotNull(ex);
            Assert.True(ex.StatusCode == 404);
            Assert.True(ex.URL == profile.AvatarFullURL);
        }

        [Fact]
        public void InvalidUrl_ThrowsEmptyResponseException()
        {
            Assert.Throws<AggregateException>(() => {
                byte[] smallBytes = SteamApiClient.GetProfilePicBytesAsync("www.liiba.laaba.com/pic.xD")
                   .Result;
            });
        }

        [Theory]
        [InlineData(76561197960321706)]
        [InlineData(76561198096280303)]
        [InlineData(76561198038389598)]
        [InlineData(76561198049597855)]
        public void ValidId_GetsProfilePicAsByteArray(ulong id64)
        {
            var profile = SteamApiClient.GetSteamAccountAsync(id64)
                .Result;

            byte[] smallPicBytes = SteamApiClient.GetProfilePicBytesAsync(profile.AvatarSmallURL)
                .Result;
            SleepAfterSendingRequest(timeout: 100);

            byte[] mediumPicBytes = SteamApiClient.GetProfilePicBytesAsync(profile.AvatarMediumURL)
                .Result;
            SleepAfterSendingRequest(timeout: 100);

            byte[] fullPicBytes = SteamApiClient.GetProfilePicBytesAsync(profile.AvatarFullURL)
                .Result;
            SleepAfterSendingRequest(timeout: 100);

            Assert.NotEmpty(smallPicBytes);
            Assert.NotEmpty(mediumPicBytes);
            Assert.NotEmpty(fullPicBytes);
        }
    }
}
