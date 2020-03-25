using SteamApi;
using SteamApi.Responses;
using System;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using Xunit;

namespace Client
{
    public class ClientFixture : IDisposable
    {
        private readonly Setup _setup;
        public string DevKey => _setup.DeveloperKey;
        public bool SleepAfterApiCall => _setup.SleepAfterApiCall;
        public int Timeout => _setup.Timeout;

        private readonly string _setupFile = "clientTestSetup.json";

        /// <summary>
        /// Reads test setup file and deserializes its contents to
        /// private setup field. Sets API key.
        /// </summary>
        public ClientFixture()
        {
            string setUpFileContent = File.ReadAllText(_setupFile);
            _setup = JsonSerializer.Deserialize<Setup>(setUpFileContent);
            ApiClient.SetApiKey(_setup.DeveloperKey);
        }


        public void Dispose() { } // Nothing to dispose

        /// <summary>
        /// JSON setup file model
        /// </summary>
        private class Setup
        {
            [JsonPropertyName("timeout")]
            public int Timeout { get; set; }
            [JsonPropertyName("developerKey")]
            public string DeveloperKey { get; set; }
            [JsonPropertyName("sleepAfterApiCall")]
            public bool SleepAfterApiCall { get; set; }
        }
    }

    /// <summary>
    /// Base class for api client tests
    /// </summary>
    public class ApiTests : IClassFixture<ClientFixture>
    {
        protected DotaApiClient DotaApiClient { get; }
        protected SteamApiClient SteamApiClient { get; }
        protected ClientFixture Fixture { get; set; }

        /// <summary>
        /// Instatiates API client objects
        /// </summary>
        /// <param name="fixture"></param>
        public ApiTests(ClientFixture fixture)
        {
            Fixture = fixture;
            DotaApiClient = new DotaApiClient();
            SteamApiClient = new SteamApiClient();
        }

        /// <summary>
        /// Makes program wait a bit after sending API request.
        /// This way running tests won't bombard valve's servers too much.
        /// </summary>
        protected virtual void SleepAfterSendingRequest()
        {
            if (Fixture.SleepAfterApiCall)
            {
                Thread.Sleep(Fixture.Timeout);
            }
        }

        /// <summary>
        /// Makes program wait a bit after sending API request.
        /// This way running tests won't bombard valve's servers too much.
        /// </summary>
        /// <param name="timeout"></param>
        protected virtual void SleepAfterSendingRequest(int timeout)
        {
            if (Fixture.SleepAfterApiCall)
            {
                Thread.Sleep(timeout);
            }
        }


        /// <summary>
        /// Asserts that request failed.
        /// </summary>
        /// <param name="response">API response object</param>
        protected static void AssertRequestFailed(IApiResponse response)
        {
            Assert.False(response.Successful);
            Assert.False(response.WasCancelled);
            Assert.NotNull(response.ThrownException);
        }


        /// <summary>
        /// Asserts that request was successful.
        /// </summary>
        /// <param name="response">API response object</param>
        protected static void AssertRequestWasSuccessful(IApiResponse response)
        {
            Assert.True(response.Successful);
            Assert.False(response.WasCancelled);
            Assert.Null(response.ThrownException);
        }


        /// <summary>
        /// Asserts that request was cancelled.
        /// </summary>
        /// <param name="response">API response object</param>
        protected static void AssertRequestWasCancelled(IApiResponse response)
        {
            Assert.False(response.Successful);
            Assert.True(response.WasCancelled);
            Assert.NotNull(response.ThrownException);
            Assert.True(response.ThrownException is OperationCanceledException);
        }
    }
}
