using System.Collections.Generic;

namespace SteamApiClient.Models
{
    public class Interface
    {
        public string Name { get; set; }
        public List<Method> Methods { get; set; }
    }
}
