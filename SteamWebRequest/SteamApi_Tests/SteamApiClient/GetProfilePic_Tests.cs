using System;
using Xunit;
using SteamApi;

namespace Client.Steam
{
    public class GetProfilePic_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetProfilePic_Tests(ClientFixture fixture) : base(fixture) {}

        /// <summary>
        /// Test case for invalid file name. Method should
        /// throw ApiResourceNotFoundException
        /// </summary>
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


        /// <summary>
        /// Test case for invalid url. Method should
        /// throw exception.
        /// </summary>
        [Fact]
        public void InvalidUrl_ThrowsEmptyResponseException()
        {
            Assert.Throws<AggregateException>(() => {
                byte[] smallBytes = SteamApiClient.GetProfilePicBytesAsync("www.liiba.laaba.com/pic.xD")
                   .Result;
            });
        }


        /// <summary>
        /// Test case for valid steam profile avatar url.
        /// Method should return correct image as byte array.
        /// </summary>
        /// <param name="id64">64-bit steam id</param>
        [Theory]
        [InlineData(76561197960321706)]
        [InlineData(76561198096280303)]
        [InlineData(76561198038389598)]
        [InlineData(76561198049597855)]
        public void ValidUrl_GetsProfilePicAsByteArray(ulong id64)
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
