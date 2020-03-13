using System;
using Xunit;
using SteamApi;
using SteamApi.Models.Steam;

namespace Client.Steam
{
    public class GetAppNews_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetAppNews_Tests(ClientFixture fixture) : base(fixture) {}


        /// <summary>
        /// Test case where provided app id is invalid. Because server
        /// response is nonsensical and returns reponse with Forbidden HTTP
        /// status code, method should internally handle that exception and
        /// throw new EmptyApiResultException.
        /// </summary>
        [Fact]
        public void InvalidId_ThrowsEmptyApiResultException()
        {
            var ex = Assert.Throws<AggregateException>(() =>
            {
                var response = SteamApiClient.GetAppNewsAsync(0)
                    .Result;
            }).InnerException as ApiEmptyResultException<AppNewsCollection>;
            SleepAfterSendingRequest();

            Assert.NotNull(ex);
            Assert.True(typeof(AppNewsCollection) == ex.ResponseModelType);
        }


        /// <summary>
        /// Test case where end date for news items is defined.
        /// Method should return news up to defined end date.
        /// </summary>
        /// <param name="appId">game app id</param>
        /// <param name="timestamp">unixtimestamp of end date</param>
        [Theory]
        [InlineData(440, 1457449513)] // TF2, 2016
        [InlineData(730, 1515423913)] // CSGO, 2018
        [InlineData(570, 1546959913)] // dota 2, 2019
        [InlineData(570, 1578495913)] // dota 2, 2020
        public void TimestampDefined_ReturnsNewsItemsWithCorrectTimetamp(uint appId, long timestamp)
        {
            var response = SteamApiClient.GetAppNewsAsync(appId, endDateTimestamp: timestamp)
                .Result;
            SleepAfterSendingRequest();

            Assert.All(response.NewsItems, item => {
                Assert.True(timestamp >= (long)item.Date);
            });
        }


        /// <summary>
        /// Test case where news item count is defined. Method
        /// should return correct amount of news items
        /// </summary>
        /// <param name="appId">game app id</param>
        /// <param name="count">news item count</param>
        [Theory]
        [InlineData(440, 0)] // TF2
        [InlineData(440, 20)] // TF2
        [InlineData(730, 10)] // CSGO
        [InlineData(570, 50)] // dota 2
        [InlineData(203770, 150)] // Crusader Kings II
        public void CountDefiend_ReturnsCorrectAmountOfNewsItems(uint appId, uint count)
        {
            var response = SteamApiClient.GetAppNewsAsync(appId, count: count)
                .Result;
            Assert.True(count == response.NewsItems.Count);
        }


        /// <summary>
        /// Test case for Valve game. Method should return app news collection
        /// about requested game.
        /// </summary>
        /// <param name="appId">Game's app id</param>
        [Theory]
        [InlineData(440)] // TF2
        [InlineData(730)] // CSGO
        [InlineData(570)] // dota 2
        [InlineData(203770)] // Crusader Kings II
        public void ValidAppId_ReturnsAppNewsAboutApp(uint appId)
        {
            var response = SteamApiClient.GetAppNewsAsync(appId)
                .Result;
            SleepAfterSendingRequest();

            Assert.Equal(appId, response.AppId);
            Assert.NotEmpty(response.NewsItems);
            Assert.True(response.TotalCount > 0);
        }
    }
}
