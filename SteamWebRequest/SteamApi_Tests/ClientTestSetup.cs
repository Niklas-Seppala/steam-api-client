using System.IO;
using System.Threading;
using System.Text.Json;
using System.Text.Json.Serialization;
using SteamApi;
using System;
using Xunit;

namespace Client
{
    public class ClientFixture : IDisposable
    {
        private Setup _setup;
        public string DevKey => _setup.DeveloperKey;
        public bool SleepAfterApiCall => _setup.SleepAfterApiCall;
        public int Timeout => _setup.Timeout;

        private string _setupFile = "clientTestSetup.json";

        public ClientFixture()
        {
            string setUpContent = File.ReadAllText(_setupFile);
            _setup = JsonSerializer.Deserialize<Setup>(setUpContent);
            ApiClient.SetDeveloperKey(_setup.DeveloperKey);
        }

        public void Dispose()
        {}

        class Setup
        {
            [JsonPropertyName("timeout")]
            public int Timeout { get; set; }
            [JsonPropertyName("developerKey")]
            public string DeveloperKey { get; set; }
            [JsonPropertyName("sleepAfterApiCall")]
            public bool SleepAfterApiCall { get; set; }
        }
    }

    public class SteamApiClientTests : IClassFixture<ClientFixture>
    {
        protected DotaApiClient Client { get; } = new DotaApiClient();
        protected ClientFixture Fixture { get; set; }


        public SteamApiClientTests(ClientFixture fixture)
        {
            Fixture = fixture;
        }

        protected virtual void SleepAfterApiCall()
        {
            if (Fixture.SleepAfterApiCall)
            {
                Thread.Sleep(Fixture.Timeout);
            }
        }

        protected virtual void SleepAfterApiCall(int timeout)
        {
            if (Fixture.SleepAfterApiCall)
            {
                Thread.Sleep(timeout);
            }
        }
    }
}
