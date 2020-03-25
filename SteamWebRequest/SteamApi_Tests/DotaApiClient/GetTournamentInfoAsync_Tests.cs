using System;
using Xunit;
using System.Threading.Tasks;
using System.Threading;

namespace Client.Dota
{
    /// <summary>
    /// Test class for dota 2 api client's GetTournamentInfo method
    /// </summary>
    public class GetTournamentInfoAsync_Tests : ApiTests
    {
        /// <summary>
        /// Setup
        /// </summary>
        public GetTournamentInfoAsync_Tests(ClientFixture fixture) : base(fixture) { }


        /// <summary>
        /// Test case for request method being cancelled by CancellationToken.
        /// Method should return failed ApiResponse object that contains thrown
        /// cancellation exception.
        /// </summary>
        [Fact]
        public async Task MethodGotCancelled_RequestFails()
        {
            CancellationTokenSource source = new CancellationTokenSource();

            // Start task to be cancelled
            var task = Task.Run(async () =>
            {
                return await DotaApiClient.GetTournamentInfoAsync(cToken: source.Token);
            });

            // Cancel method
            source.Cancel();

            var response = await task;
            SleepAfterSendingRequest();

            AssertRequestWasCancelled(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case for invalid API method version being provided.
        /// Method should return failed ApiResponse object where exception
        /// that caused failure is stored.
        /// </summary>
        [Fact(Skip = "arbitrary version doesn't matter??")]
        public void InvalidMethodVersion_RequestFails()
        {
            var response = DotaApiClient.GetTournamentInfoAsync(version: "v1.3")
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
        }


        /// <summary>
        /// Test case where max tier is lower than min tier. Method should
        /// throw argument exception.
        /// </summary>
        [Fact]
        public void MinMaxTierParamsClash_ThrowsArgumentException()
        {
            var response = DotaApiClient.GetTournamentInfoAsync(minTier: 6)
                .Result;
            SleepAfterSendingRequest();

            AssertRequestFailed(response);
            Assert.Null(response.Contents);
            Assert.True(response.ThrownException is ArgumentException);
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

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response);
            Assert.NotEmpty(response.Contents);
            Assert.All(response.Contents, tournament => {
                Assert.NotNull(tournament);
                Assert.True(tournament.StartTimestamp 
                    > (ulong)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
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

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
            Assert.All(response.Contents, tournament => {
                Assert.NotNull(tournament);
                Assert.True(tournament.StartTimestamp > (ulong)timestamp);
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

            AssertRequestWasSuccessful(response);
            Assert.NotEmpty(response.Contents);
            Assert.All(response.Contents, tournament => {
                Assert.NotNull(tournament);
                Assert.True(tournament.StartTimestamp > (ulong)timestamp);
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

            AssertRequestWasSuccessful(response);
            Assert.NotNull(response.Contents);
            Assert.NotEmpty(response.Contents);
            Assert.All(response.Contents, tournament => {
                Assert.True(tournament.Tier >= minTier);
            });
        }
    }
}
