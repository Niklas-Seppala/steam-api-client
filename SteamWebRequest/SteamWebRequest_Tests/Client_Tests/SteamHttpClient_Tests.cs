using SteamApiClient;
using System;
using System.Threading;

namespace SWR.Client_Tests
{
    public class SteamHttpClient_Tests
    {
        protected static SteamHttpClient _client = GlobalSetup.Client;


        protected virtual void Sleep()
        {
            if (GlobalSetup.SleepAfterApiCall)
            {
                Thread.Sleep(GlobalSetup.Timeout);
            }
        }

        protected virtual void Sleep(int timeout)
        {
            if (GlobalSetup.SleepAfterApiCall)
            {
                Thread.Sleep(timeout);
            }
        }
    }
}
