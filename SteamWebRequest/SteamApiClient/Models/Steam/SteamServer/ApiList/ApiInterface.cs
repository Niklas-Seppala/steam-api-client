using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public class ApiInterface
    {
        public string Name { get; set; }
        public List<Method> Methods { get; set; }
    }
}
