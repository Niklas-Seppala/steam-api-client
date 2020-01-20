using System.IO;
using System;

namespace SWR
{
    internal static class SecretVariables
    {
        public static string DevKey { get; } = ReadFromFile("devkey");

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
