using System;
using Xunit;
using System.Linq;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetTournamentInfo method
    /// </summary>
    public class GetTournamentInfoAsync : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetTournamentInfoAsync(ClientFixture fixture) : base(fixture) { }


        /// <summary>
        /// Test case where max tier is lower than min tier. Method should
        /// throw argument exception.
        /// </summary>
        [Fact]
        public void MinMaxTierParamsClash_ThrowsArgumentException()
        {
            var ex = Assert.Throws<AggregateException>(() => {
                var response = DotaApiClient.GetTournamentInfoAsync(minTier: 6)
                    .Result;
            });

            var inner = ex.InnerException as ArgumentException;
            Assert.NotNull(inner);
        }


        /// <summary>
        /// Test case for default parameters. Method should
        /// use current datetime as starting point for query.
        /// </summary>
        [Fact]
        public void DefaultParameter_ReturnsTournamentInfoFromCurrentDate()
        {
            var response = DotaApiClient.GetTournamentInfoAsync()
                .Result;
            SleepAfterSendingRequest();

            Assert.NotEmpty(response);
            Assert.All(response, t => {
                Assert.NotNull(t);
                Assert.True(t.StartTimestamp > (ulong)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
            });
        }


        /// <summary>
        /// Test case where start timestamp is defined. Method
        /// should return events that start/started after the
        /// specified timestamp.
        /// </summary>
        /// <param name="timestamp">unix epoch timestamp</param>
        [Theory]
        [InlineData(1457866851)]
        [InlineData(1489402851)]
        [InlineData(1520938851)]
        [InlineData(1552474851)]
        [InlineData(1578913251)]
        public void TimestampDefined_ReturnsTournamentsFromProvidedTimestamp(long timestamp)
        {
            var response = DotaApiClient.GetTournamentInfoAsync(timestamp)
                .Result;
            SleepAfterSendingRequest();

            Assert.NotEmpty(response);
            Assert.All(response, t => {
                Assert.NotNull(t);
                Assert.True(t.StartTimestamp > (ulong)timestamp);
            });
        }


        /// <summary>
        /// Test case where start date is defined. Method
        /// should return events that start/started after the
        /// specified date.
        /// </summary>
        /// <param name="timestamp">unix epoch timestamp</param>
        [Theory]
        [InlineData(1457866851)] // parameter must be defined at compile time
        [InlineData(1489402851)]
        [InlineData(1520938851)]
        [InlineData(1552474851)]
        [InlineData(1578913251)]
        public void DateDefined_ReturnsTournamentsFromProvidedDate(long timestamp)
        {
            // convert compile time argument to Datetime object
            DateTime date = new DateTime(1970, 1, 1) + TimeSpan.FromSeconds(timestamp);

            var response = DotaApiClient.GetTournamentInfoAsync(date)
                .Result;
            SleepAfterSendingRequest();

            Assert.NotEmpty(response);
            Assert.All(response, t => {
                Assert.NotNull(t);
                Assert.True(t.StartTimestamp > (ulong)timestamp);
            });
        }


        /// <summary>
        /// Test case where min tournament tier is defined. Method should
        /// return tournament above or at specified tier.
        /// </summary>
        /// <param name="minTier">minimum tier included</param>
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void MinTierDefined_ReturnsTournamentsAboveOrAtMinTier(uint minTier)
        {
            var response = DotaApiClient.GetTournamentInfoAsync(timestamp: 1489402851, minTier: minTier)
                .Result;
            Assert.All(response, tournament => {
                Assert.True(tournament.Tier >= minTier);
            });
        }
    }
}
