using SteamApiClient;
using System;
using System.IO;
using System.Threading;

namespace SWR
{
    internal static class GlobalSetup
    {
        public static string DevKey { get; } = ReadFromFile("devkey");
        public static SteamHttpClient Client { get; } = new SteamHttpClient(DevKey);
        public static bool SleepAfterApiCall { get; set; } = true;
        public static int Timeout { get; set; } = 300;

        private static string ReadFromFile(string keyword)
        {
            try
            {
                string[] lines = File.ReadAllLines("SECRETS.dat");
                foreach (string line in lines)
                {
                    if (line.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    {
                        return line.Split("=")[1];
                    }
                }
                throw new Exception("Keyword not found.");
            }
            catch
            {
                throw;
            }
        }
    }
}
