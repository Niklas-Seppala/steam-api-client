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
        /// return null content wrapped into ApiResponseModel
        /// </summary>
        [Fact]
        public void InvalidFileName_ThrowsHttpRequestException()
        {
            var profile = SteamApiClient.GetSteamAccountAsync(76561197960321706)
               .Result;
            SleepAfterSendingRequest();

            // Mess up the profile avatar URL
            if (profile.Contents.AvatarFullURL.Contains("jpg"))
                profile.Contents.AvatarFullURL = profile.Contents.AvatarFullURL.Replace("jpg", "png");
            else
                profile.Contents.AvatarFullURL = profile.Contents.AvatarFullURL.Replace("png", "jpg");

            var response = SteamApiClient.GetProfilePicBytesAsync(profile.Contents.AvatarFullURL)
                   .Result;
            SleepAfterSendingRequest();


            Assert.False(response.Successful);
            Assert.Null(response.Contents);
            Assert.NotNull(response.ThrownException);
            Assert.True(response.ThrownException is ApiResourceNotFoundException);
        }


        /// <summary>
        /// Test case for invalid url. Method should
        /// return null content wrapped into ApiResponse model.
        /// </summary>
        [Fact]
        public void InvalidUrl_ThrowsEmptyResponseException()
        {
            var response = SteamApiClient.GetProfilePicBytesAsync("https://www.liibalaaba.com/pic.xf")
                .Result;
            SleepAfterSendingRequest();

            Assert.False(response.Successful);
            Assert.Null(response.Contents);
            Assert.NotNull(response.ThrownException);
        }


        /// <summary>
        /// Test case for valid steam profile avatar url.
        /// Method should return correct image as byte array
        /// wrapped into ApiResponse object.
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
            SleepAfterSendingRequest();

            var smallPicBytes = SteamApiClient.GetProfilePicBytesAsync(profile.Contents.AvatarSmallURL)
                .Result;
            SleepAfterSendingRequest(timeout: 0);

            var mediumPicBytes = SteamApiClient.GetProfilePicBytesAsync(profile.Contents.AvatarMediumURL)
                .Result;
            SleepAfterSendingRequest(timeout: 0);

            var fullPicBytes = SteamApiClient.GetProfilePicBytesAsync(profile.Contents.AvatarFullURL)
                .Result;
            SleepAfterSendingRequest(timeout: 0);

            Assert.NotEmpty(smallPicBytes.Contents);
            Assert.NotEmpty(mediumPicBytes.Contents);
            Assert.NotEmpty(fullPicBytes.Contents);
        }
    }
}
