using System.Collections.Generic;

namespace SteamApi.Models
{
    public class ApiInterface
    {
        public string Name { get; set; }
        public List<Method> Methods { get; set; }
    }
}
